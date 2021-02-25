using System.Collections.Generic;
using Castle.Core.Internal;
using xyz.lsyyy.Verification.Data;

namespace xyz.lsyyy.Verification
{
	public class ActionTagStatus
	{
		public bool IsClean => Deleted.IsNullOrEmpty() && New.IsNullOrEmpty() && Modified.IsNullOrEmpty();

		/// <summary>
		/// 正常
		/// </summary>
		public IEnumerable<ActionTag> Normal { get; set; }

		/// <summary>
		/// 已删除
		/// </summary>
		public IEnumerable<ActionTag> Deleted { get; set; }

		/// <summary>
		/// 新增
		/// </summary>
		public IEnumerable<ActionTagModel> New { get; set; }

		/// <summary>
		/// 已修改
		/// </summary>
		public IEnumerable<ModifiedActionTag> Modified { get; set; }
	}

	public class ModifiedActionTag
	{
		/// <summary>
		/// 新
		/// </summary>
		public ActionTagModel New { get; set; }

		/// <summary>
		/// 旧
		/// </summary>
		public ActionTag Old { get; set; }
	}
}
