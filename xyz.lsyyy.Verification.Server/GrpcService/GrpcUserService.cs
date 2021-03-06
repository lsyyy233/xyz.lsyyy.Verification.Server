using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Model;
using xyz.lsyyy.Verification.Protos;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification
{
	/// <summary>
	/// GRPC服务端
	/// </summary>
	public class GrpcUserService : UserRpcService.UserRpcServiceBase
	{
		private readonly MyDbContext db;
		private readonly TokenService tokenService;

		public GrpcUserService(MyDbContext db, TokenService tokenService)
		{
			this.db = db;
			this.tokenService = tokenService;
		}

		public override async Task<GetUserResponse> GetCurrentUser(GetUserRequesr request, ServerCallContext context)
		{
			GetUserIdByTokenResult result = await tokenService.GetUserIdByTokenAsync(request.Token);
			GetUserResponse response = new GetUserResponse();
			if (result.Exist)
			{
				User user = await db.Users.FirstOrDefaultAsync(x => x.Id == result.UserId);
				if (user != null)
				{
					response.Id = user.Id;
					response.Name = user.Name;
					response.DepartmentId = user.PositionId ?? -1;
				}

			}
			return response;
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
				//已登录的用户不是管理员用户
				if (await db.Users.AnyAsync(x => x.Id == request.CurrentUserId && x.PositionId == null))
				{
					await db.Users.AddAsync(new User
					{
						Name = request.UserName,
						Password = request.Password
					});
				}
				else
				{
					return ResponseHelper.BadResponse("需要管理员用户");
				}

				if (await db.Users.AnyAsync(x => x.Name == request.UserName))
				{
					return ResponseHelper.BadResponse("用户名已存在");
				}

			}

			if (await db.SaveChangesAsync() > 0)
			{
				return ResponseHelper.OkResponse();
			}
			return ResponseHelper.BadResponse();
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
				string token = await tokenService.AddToken(user.Id);
				return new UserLoginResponse
				{
					IsSuccess = true,
					Token = token
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
			if (!await db.Positions.AnyAsync(x => x.Id == request.PositionId))
			{
				return ResponseHelper.BadResponse("指定的职位不存在");
			}
			if (string.IsNullOrWhiteSpace(request.Name))
			{
				return ResponseHelper.BadResponse("用户名不能为空");
			}
			if (await db.Users.AnyAsync(x => x.Name == request.Name))
			{
				return ResponseHelper.BadResponse("用户名已存在");
			}
			await db.Users.AddAsync(
				new User
				{
					Name = request.Name,
					PositionId = request.PositionId,
					Password = request.Password
				});
			int result = await db.SaveChangesAsync();
			if (result <= 0)
			{
				return ResponseHelper.BadResponse();
			}
			return new GeneralResponse
			{
				IsSuccess = true
			};
		}
	}
}
