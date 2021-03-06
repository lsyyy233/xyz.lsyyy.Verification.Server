using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Protos;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification
{
	public class GrpcActionService : ActionRpcService.ActionRpcServiceBase
	{
		private readonly MyDbContext db;

		public GrpcActionService(MyDbContext db)
		{
			this.db = db;
		}

		/// <summary>
		/// 更新Tag
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<GeneralResponse> UpdateTag(UpdateTagRequest request, ServerCallContext context)
		{
			ActionTag actionTag = await db.ActionTags.FirstOrDefaultAsync(x => x.Id == request.TagId);
			if (actionTag == null)
			{
				return ResponseHelper.BadResponse("对应的Tag不存在");
			}
			if (!string.IsNullOrWhiteSpace(request.ActionName))
				actionTag.ActionName = request.ActionName;
			if (!string.IsNullOrWhiteSpace(request.ControllerName))
				actionTag.ControllerName = request.ControllerName;
			if (!string.IsNullOrWhiteSpace(request.TagName))
				actionTag.TagName = request.TagName;
			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}
			return ResponseHelper.BadResponse("未知错误");
		}

		/// <summary>
		/// 获取所有Tag
		/// </summary>
		/// <param name="request"></param>
		/// <param name="responseStream"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task GetAllTag(GetAllTagRequest request, IServerStreamWriter<GetAllTagResponse> responseStream, ServerCallContext context)
		{
			List<GetAllTagResponse> actionTags = await db.ActionTags
				.Select(x => new GetAllTagResponse
				{
					ActionName = x.ActionName,
					ControllerName = x.ControllerName,
					TagId = x.Id,
					TagName = x.TagName
				}).ToListAsync();
			foreach (GetAllTagResponse tag in actionTags)
			{
				await responseStream.WriteAsync(tag);
			}
		}
	}
}
