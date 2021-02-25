using System.Collections.Generic;
using System.Linq;
using xyz.lsyyy.Verification.Data;

namespace xyz.lsyyy.Verification
{
	public class MemoryActionTagService
	{
		private readonly List<ActionTagModel> Actions = new List<ActionTagModel>();

		/// <summary>
		/// 刷新内存中的Tag列表
		/// </summary>
		/// <param name="tagList"></param>
		public void FlushTagsInMemory(IEnumerable<ActionTagModel> tagList)
		{
			Actions.Clear();
			Actions.AddRange(tagList);
		}

		public List<ActionTagModel> GeMemTags()
		{
			return Actions
				.Select(x => 
					new ActionTagModel
					{
						ActionName = x.ActionName,
						ControllerName = x.ControllerName,
						Tag = x.Tag
					})
				.ToList();
		}
	}
}
