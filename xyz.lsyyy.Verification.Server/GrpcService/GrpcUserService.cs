using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Protos;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification
{
	/// <summary>
	/// GRPC服务端
	/// </summary>
	public class GrpcUserService : Protos.UserRpcService.UserRpcServiceBase
	{
		private readonly MyDbContext db;
		public GrpcUserService(MyDbContext db)
		{
			this.db = db;
		}

		public override async Task<GetUserResponse> GetUser(GeneralIdRequest request, ServerCallContext context)
		{
			Guid.TryParse(request.Id, out Guid userId);
			User user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
			return new GetUserResponse
			{
				Id = user?.Id.ToString() ?? string.Empty,
				Name = user?.Name ?? string.Empty,
				PositionId = user == null
					? string.Empty
					: user.PositionId.HasValue ? string.Empty : user.PositionId.ToString()

			};
		}

		public override async Task<GeneralResponse> RegistAdminUser(RegistAdminUserRequest request, ServerCallContext context)
		{
			//如果不存在管理员用户(即没有部门的用户)，添加用户到数据库
			if (!await db.Users.AnyAsync(x => x.PositionId == null))
			{
				await db.Users.AddAsync(new User
				{
					Name = request.UserName,
					Password = request.Password
				});
			}
			else
			{
				//用户Id不合法
				if (!Guid.TryParse(request.CurrentUserId, out Guid userId))
				{
					return ResponseHelper.Response(false, "需要管理员用户");
				}
				//已登录的用户不是管理员用户
				if (await db.Users.AnyAsync(x => x.Id == userId && x.PositionId == null))
				{
					await db.Users.AddAsync(new User
					{
						Name = request.UserName,
						Password = request.Password
					});
				}
				else
				{
					return ResponseHelper.Response(false, "需要管理员用户");
				}

				if (await db.Users.AnyAsync(x => x.Name == request.UserName))
				{
					return ResponseHelper.Response(false, "用户名已存在");
				}

			}

			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.Response();
			}
			return ResponseHelper.Response(false);
		}

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<UserLoginResponse> UserLogin(UserLoginRequest request, ServerCallContext context)
		{
			User user =
				await db.Users.FirstOrDefaultAsync(x =>
					x.Name == request.UserName && x.Password == HashHelper.GetSha256WithSalt(request.Password));
			if (user != null)
			{
				return new UserLoginResponse
				{
					IsSuccess = true,
					UserInfo = new UserInfo()
					{
						Id = user.Id.ToString(),
						Name = user.Name,
						PositionId = user.PositionId.ToString()
					}
				};
			}
			return new UserLoginResponse
			{
				IsSuccess = false
			};
		}

		/// <summary>
		/// 用户注册
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<GeneralResponse> RegistUser(RegistUserRequest request, ServerCallContext context)
		{
			Guid PositionId = Guid.Parse(request.PositionId);
			if (!await db.Positions.AnyAsync(x => x.Id == PositionId))
			{
				return ResponseHelper.Response(false, "指定的职位不存在");
			}
			if (string.IsNullOrWhiteSpace(request.Name))
			{
				return ResponseHelper.Response(false, "用户名不能为空");
			}
			if (await db.Users.AnyAsync(x => x.Name == request.Name))
			{
				return ResponseHelper.Response(false, "用户名已存在");
			}
			await db.Users.AddAsync(
				new User
				{
					Name = request.Name,
					PositionId = PositionId,
					Password = request.Password
				});
			int result = await db.SaveChangesAsync();
			if (result <= 0)
			{
				return ResponseHelper.Response(false, "未知错误");
			}
			return new GeneralResponse
			{
				IsSuccess = true
			};
		}
	}
}
