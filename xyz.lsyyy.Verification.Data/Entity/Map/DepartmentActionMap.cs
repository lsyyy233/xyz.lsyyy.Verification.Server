namespace xyz.lsyyy.Verification.Data
{
	public class DepartmentActionMap : BaseMap
	{
		public int DepartmentId { get; set; }

		public virtual Department Department { get; set; }
	}
}
