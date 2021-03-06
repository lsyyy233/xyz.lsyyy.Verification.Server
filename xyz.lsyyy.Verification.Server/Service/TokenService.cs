using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Model;

namespace xyz.lsyyy.Verification
{
	public class TokenService
	{
		private readonly IDistributedCache cache;
		private readonly DistributedCacheEntryOptions option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(6));

		public TokenService(IDistributedCache cache)
		{
			this.cache = cache;
		}

		public async Task<string> AddToken(int userId)
		{
			string token = Guid.NewGuid().ToString();
			await cache.SetStringAsync(token, userId.ToString(), option);
			return token;
		}

		public async Task<GetUserIdByTokenResult> GetUserIdByTokenAsync(string token)
		{
			string userIdStr = await cache.GetStringAsync(token);
			bool b = int.TryParse(userIdStr,out int userId);
			return new GetUserIdByTokenResult
			{
				Exist = b,
				UserId = userId
			};
		}

		public async Task Cancellation(string token)
		{
			await cache.RemoveAsync(token);
		}

	}
}
