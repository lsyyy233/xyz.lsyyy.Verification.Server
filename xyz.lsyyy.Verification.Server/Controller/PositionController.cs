using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Model;

namespace xyz.lsyyy.Verification.Controller
{
	[ApiController]
	[Route("api/v1/[Controller]")]
	public class PositionController : ControllerBase
	{
		private readonly MyDbContext db;
		public PositionController(MyDbContext db)
		{
			this.db = db;
		}
		/// <summary>
		/// 添加职位
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<object> AddPositionAsync(PositionAddModel model)
		{
			if (model.SuperiorPositionId.HasValue)
			{
				if (!await db.Positions.AnyAsync(x => x.Id == model.SuperiorPositionId.Value))
				{
					return BadRequest("指定的上级职位不存在");
				}
			}

			if (!await db.Departments.AnyAsync(x => x.Id == model.DepartmentId))
			{
				return BadRequest("指定的部门不存在");
			}
			Position position = new Position
			{
				Id = new Guid(),
				Name = model.PositionName,
				SuperiorPositionId = model.SuperiorPositionId,
				DepartmentId = model.DepartmentId
			};
			await db.AddAsync(position);
			if (await db.SaveChangesAsync() > 0)
			{
				return Ok();
			}
			return StatusCode(500, "添加失败");
		}
	}
}
