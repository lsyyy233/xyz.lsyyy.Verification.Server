﻿using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Model;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Services
{
	public class GrpcVerificationService : VerificationRpcService.VerificationRpcServiceBase
	{
		private readonly MyDbContext db;
		private readonly TokenService tokenService;

		public GrpcVerificationService(MyDbContext db, TokenService tokenService)
		{
			this.db = db;
			this.tokenService = tokenService;
		}

		/// <summary>
		/// 用户是否允许访问Action
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<VerificationResult> GetAccess(VerificationModel request, ServerCallContext context)
		{
			GetUserIdByTokenResult result = await tokenService.GetUserIdByTokenAsync(request.Token);
			if (!result.Exist)
				return AccessResult(false);
			//如果是管理员用户，返回允许
			if (await db.Users.AnyAsync(x => x.Id == result.UserId && x.PositionId == null))
			{
				return AccessResult(true);
			}
			//Tag不存在时，返回不允许
			ActionTag tag = await (
				from a in db.ActionTags
				where a.TagName == request.TagName
				select a).FirstOrDefaultAsync();
			if (tag == null)
			{
				return AccessResult(false);
			}
			//存在针对单个用户新增的该Tag的访问权限，允许访问
			if (await db.UserActionMaps.AnyAsync(x => x.UserId == result.UserId && x.ActionTagId == tag.Id && x.AccessType == UserActionMapType.Add))
			{
				return AccessResult(true);
			}
			//如过用户的职位允许访问该tag,返回允许
			Position userPosition = await (
				from u in db.Users
				join p in db.Positions on u.PositionId equals p.Id
				select p).FirstOrDefaultAsync();
			if (await db.PositionActionMaps.AnyAsync(x => x.PositionId == userPosition.Id && x.ActionTagId == tag.Id))
			{
				return AccessResult(true);
			}
			//如过用户的部门允许访问该tag,返回允许
			Department department = await db.Departments.FirstOrDefaultAsync(x => x.Id == userPosition.DepartmentId);
			if (await db.DepartmentActionMaps.AnyAsync(x => x.DepartmentId == department.Id && x.ActionTagId == tag.Id))
			{
				return AccessResult(true);
			}
			return AccessResult(false);
		}

		private VerificationResult AccessResult(bool access)
		{
			return new VerificationResult
			{
				Access = access
			};
		}
	}
}
