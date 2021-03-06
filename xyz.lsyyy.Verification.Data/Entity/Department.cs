using System;

namespace xyz.lsyyy.Verification.Data
{
	public class Department
	{
		/// <summary>
		/// 部门Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 部门名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 上级部门Id
		/// </summary>
		public int? SuperiorDepartmentId { get; set; }

		/// <summary>
		/// 上级部门
		/// </summary>
		public virtual Department SuperiorDepartment { get; set; }
	}
}
