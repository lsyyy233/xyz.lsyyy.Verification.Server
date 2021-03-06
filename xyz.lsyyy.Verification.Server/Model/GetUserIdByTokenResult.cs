namespace xyz.lsyyy.Verification.Model
{
	public class GetUserIdByTokenResult
	{
		/// <summary>
		/// 是否存在Token
		/// </summary>
		public bool Exist { get; set; }

		/// <summary>
		/// 用户Id
		/// </summary>
		public int UserId { get; set; }
	}
}
