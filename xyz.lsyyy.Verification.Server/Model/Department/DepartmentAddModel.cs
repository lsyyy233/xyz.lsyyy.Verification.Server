using System;

namespace xyz.lsyyy.Verification.Model
{
	public class DepartmentAddModel
	{
		/// <summary>
		/// 部门名称
		/// </summary>
		public string DepartmentName { get; set; }

		/// <summary>
		/// 上级部门ID
		/// </summary>
		public Guid? SuperiorDepartmentId { get; set; }
	}
}
