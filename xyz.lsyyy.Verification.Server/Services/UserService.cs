using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification
{
	/// <summary>
	/// GRPC服务端
	/// </summary>
	public class UserService : Protos.User.UserBase
	{
		private readonly MyDbContext db;
		public UserService(MyDbContext db)
		{
			this.db = db;
		}

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<LoginResponse> UserLogin(LoginRequest request, ServerCallContext context)
		{
			Data.User user =
				await db.Users.FirstOrDefaultAsync(x => x.Name == request.Name && x.Password == request.Password);
			if (user != null)
			{
				return new LoginResponse
				{
					Success = true,
					UserResponse = new UserResponse
					{
						Id = user.Id.ToString(),
						Name = user.Name,
						PositionId = user.PositionId.ToString()
					}
				};
			}
			return new LoginResponse
			{
				Success = false
			};
		}

		/// <summary>
		/// 用户注册
		/// </summary>
		/// <param name="request"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<RegistUserResponse> RegistUser(RegistUserRequest request, ServerCallContext context)
		{
			Guid PositionId = Guid.Parse(request.PositionId);
			if (!await db.Positions.AnyAsync(x => x.Id == PositionId))
			{
				return new RegistUserResponse
				{
					Success = false,
					Message = "指定的职位不存在"
				};
			}
			if (string.IsNullOrWhiteSpace(request.Name))
			{
				return new RegistUserResponse
				{
					Success = false,
					Message = "用户名不能为空"
				};
			}
			if(await db.Users.AnyAsync(x => x.Name == request.Name))
			{
				return new RegistUserResponse
				{
					Success = false,
					Message = "用户名已存在"
				};
			}
			Data.User user = new Data.User
			{
				Id = new Guid(),
				Name = request.Name,
				PositionId = PositionId,
				Password = request.Password
			};
			await db.Users.AddAsync(user);
			int result = await db.SaveChangesAsync();
			if (result <= 0)
			{
				return new RegistUserResponse
				{
					Success = false
				};
			}
			return new RegistUserResponse
			{
				Success = true,
				UserResponse = new UserResponse
				{
					Id = user.Id.ToString(),
					Name = user.Name,
					PositionId = user.PositionId.ToString()
				}
			};
		}
	}
}
