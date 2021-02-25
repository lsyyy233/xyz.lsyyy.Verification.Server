using System;

namespace xyz.lsyyy.Verification.Data
{
	public class BaseMap
	{
		public Guid ActionId { get; set; }

		public virtual ActionTag ActionTag { get; set; }
	}
}
