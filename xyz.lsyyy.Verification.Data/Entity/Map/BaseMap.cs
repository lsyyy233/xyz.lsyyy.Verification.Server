using System;

namespace xyz.lsyyy.Verification.Data
{
	public class BaseMap
	{
		public Guid ActionTagId { get; set; }

		public virtual ActionTag ActionTag { get; set; }
	}
}
