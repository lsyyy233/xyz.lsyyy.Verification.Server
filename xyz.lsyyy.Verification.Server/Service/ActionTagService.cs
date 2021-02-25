using System.Linq;
using xyz.lsyyy.Verification.Data;

namespace xyz.lsyyy.Verification
{
	public class ActionTagService
	{
		private readonly MyDbContext db;
		private readonly MemoryActionTagService memtTagService;

		public ActionTagService(MyDbContext db, MemoryActionTagService memtTagService)
		{
			this.db = db;
			this.memtTagService = memtTagService;
		}

		//TODO获取ActionTag状态（新增、删除、修改）
		public IQueryable<ActionTag> GetAllTag()
		{
			return db.ActionTags;
		} 

	}
}
