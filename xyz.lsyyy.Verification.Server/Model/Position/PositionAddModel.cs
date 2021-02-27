using System;

namespace xyz.lsyyy.Verification.Model
{
	public class PositionAddModel
	{
		/// <summary>
		/// 职位名称
		/// </summary>
		public string PositionName { get; set; }

		/// <summary>
		/// 上级职位ID
		/// </summary>
		public Guid? SuperiorPositionId { get; set; }

		/// <summary>
		/// 职位所属部门Id
		/// </summary>
		public Guid DepartmentId { get; set; }
	}
}
