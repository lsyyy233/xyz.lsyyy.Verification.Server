﻿using System;

namespace xyz.lsyyy.Verification.Data
{
	/// <summary>
	/// 职位
	/// </summary>
	public class Position
	{
		/// <summary>
		/// 职位Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 所属部门Id
		/// </summary>
		public int DepartmentId { get; set; }

		/// <summary>
		/// 所属部门
		/// </summary>
		public virtual Department Department { get; set; }
		
		/// <summary>
		/// 上级职位Id
		/// </summary>
		public int? SuperiorPositionId { get; set; }

		/// <summary>
		/// 上级职位
		/// </summary>
		public virtual Position SuperiorPosition { get; set; }

		/// <summary>
		/// 职位名称
		/// </summary>
		public string Name { get; set; }
	}
}
