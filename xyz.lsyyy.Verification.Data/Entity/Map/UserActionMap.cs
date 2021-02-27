using System;

namespace xyz.lsyyy.Verification.Data
{
	public class UserActionMap : BaseMap
	{
		public Guid UserId { get; set; }

		public virtual User User { get; set; }

	/// <summary>
	/// 针对单个用户权限设置类型
	/// </summary>
		public UserActionMapType AccessType { get; set; }
	}

	public enum UserActionMapType
	{
		/// <summary>
		/// 增加
		/// </summary>
		Add = 1,

		/// <summary>
		/// 移除
		/// </summary>
		Remove = 2
	}
}
