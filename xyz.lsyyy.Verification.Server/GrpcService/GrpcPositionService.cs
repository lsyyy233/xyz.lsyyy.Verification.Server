using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Protos;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification
{
	public class GrpcPositionService : PositionRpcService.PositionRpcServiceBase
	{
		private readonly MyDbContext db;
		public GrpcPositionService(MyDbContext db)
		{
			this.db = db;
		}

		public override async Task<GeneralResponse> AddPosition(AddPositionRequest request, ServerCallContext context)
		{
			if (await db.Positions.AnyAsync(x =>
				x.DepartmentId == request.DepartmentId && x.Name == request.PositionName))
			{
				return ResponseHelper.BadResponse("部门已存在该职位");
			}

			if (request.SuperiorPositionId.HasValue)
			{
				if (await db.Positions.AnyAsync(x => x.Id == request.SuperiorPositionId))
				{
					return ResponseHelper.BadResponse("指定的上级职位不存在");
				}
			}

			await db.Positions.AddAsync(new Position
			{
				Name = request.PositionName,
				DepartmentId = request.DepartmentId,
				SuperiorPositionId = request.SuperiorPositionId
			});
			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}
			return ResponseHelper.BadResponse("未知错误");
		}
	}
}
