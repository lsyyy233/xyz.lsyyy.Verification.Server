using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Protos;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification
{
	public class GrpcDepartmentService : DepartmentRpcService.DepartmentRpcServiceBase
	{
		private readonly MyDbContext db;
		public GrpcDepartmentService(MyDbContext db)
		{
			this.db = db;
		}

		/// <summary>
		/// 添加部门
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<GeneralResponse> AddDepartment(AddDepartmentRequest request, ServerCallContext context)
		{
			if (await db.Departments.AnyAsync(x => x.Name == request.DepartmentName))
			{
				return ResponseHelper.BadResponse("部门名称已存在");
			}

			if (request.SuperiorDepartmentId.HasValue)
			{
				if (!await db.Departments.AnyAsync(x => x.Id == request.SuperiorDepartmentId.Value))
				{
					return ResponseHelper.BadResponse("上级部门不存在");
				}
			}
			await db.Departments.AddAsync(new Department
			{
				Name = request.DepartmentName,
				SuperiorDepartmentId = request.SuperiorDepartmentId
			});
			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}

			return ResponseHelper.BadResponse();
		}

		/// <summary>
		/// 为部门添加访问权限
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<GeneralResponse> AddActionTag(DepartmentAddActionRequest request, ServerCallContext context)
		{
			if (await db.ActionTags.AnyAsync(x => x.Id == request.ActionTagId))
			{
				return ResponseHelper.BadResponse("Tag不存在");
			}

			if (await db.Departments.AnyAsync(x => x.Id == request.DepartmentId))
			{
				return ResponseHelper.BadResponse("部门不存在");
			}

			if (await db.DepartmentActionMaps.AnyAsync(x =>
				x.DepartmentId == request.DepartmentId && x.ActionTagId == request.ActionTagId))
			{
				return ResponseHelper.BadResponse("已存在访问权限");
			}
			await db.DepartmentActionMaps.AddAsync(new DepartmentActionMap
			{
				ActionTagId = request.ActionTagId,
				DepartmentId = request.DepartmentId
			});
			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}
			return ResponseHelper.BadResponse();
		}

		/// <summary>
		/// 移除部门的访问权限
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<GeneralResponse> DepartmentDeleteActionTag(DepartmentDeleteActionTagRequest request, ServerCallContext context)
		{
			DepartmentActionMap actionMap = await db.DepartmentActionMaps.FirstOrDefaultAsync(x =>
				x.DepartmentId == request.DepartmentId && x.ActionTagId == request.ActionTagId);
			if (actionMap == null)
			{
				return ResponseHelper.BadResponse("访问权限不存在");
			}

			db.DepartmentActionMaps.Remove(actionMap);
			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}
			return ResponseHelper.BadResponse();
		}

		/// <summary>
		/// 删除部门
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<GeneralResponse> DeleteDepartment(GeneralIdRequest request, ServerCallContext context)
		{
			Department department = await db.Departments.FirstOrDefaultAsync(x => x.Id == request.Id);
			if (department == null)
			{
				return ResponseHelper.BadResponse("指定部门不存在");
			}

			db.Departments.Remove(department);
			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}
			return ResponseHelper.BadResponse();
		}
	}
}
