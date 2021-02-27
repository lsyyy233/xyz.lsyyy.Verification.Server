using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Model;

namespace xyz.lsyyy.Verification.Controller
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class DepartmentController : ControllerBase
	{
		private readonly MyDbContext db;
		public DepartmentController(MyDbContext db)
		{
			this.db = db;
		}

		/// <summary>
		/// 添加部门
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<object> AddDepartmentAsync(DepartmentAddModel model)
		{
			Department department = new Department
			{
				Id = new Guid(),
				Name = model.DepartmentName,
				SuperiorDepartmentId = model.SuperiorDepartmentId
			};
			if (model.SuperiorDepartmentId.HasValue)
			{
				if (!await db.Departments.AnyAsync(x => x.Id == model.SuperiorDepartmentId.Value))
				{
					return BadRequest("上级部门不存在");
				}
			}
			await db.AddAsync(department);
			if (await db.SaveChangesAsync() > 0)
			{
				return Ok();
			}
			return StatusCode(500, "添加失败");
		}
	}
}