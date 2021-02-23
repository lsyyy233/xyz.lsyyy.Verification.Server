using System;

namespace xyz.lsyyy.Verification.Model
{
	public class PositionAddModel
	{
		public string PositionName { get; set; }

		public Guid? SuperiorPositionId { get; set; }

		public Guid DepartmentId { get; set; }
	}
}
