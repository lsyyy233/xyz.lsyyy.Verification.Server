using System.ComponentModel.DataAnnotations;

namespace xyz.lsyyy.Verification
{
	public class ActionTagModel
	{
		[Required]
		public string ActionName { get; set; }

		[Required]
		public string ControllerName { get; set; }

		[Required]
		public string Tag { get; set; }
	}
}