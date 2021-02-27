using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
		public async Task<string> AddTagAsync(ActionTagModel model)
		{
			if (await db.ActionTags.AnyAsync(x => x.TagName == model.Tag))
			{
				return "Tag已存在";
			}

			if (await db.ActionTags.AnyAsync(x =>
				x.ActionName == model.ActionName && x.ControllerName == model.ControllerName))
			{
				return "ControllerName和Action已存在";
			}

			await db.ActionTags.AddAsync(new ActionTag
			{
				ActionName = model.ActionName,
				ControllerName = model.ControllerName,
				TagName = model.Tag
			});
			return await db.SaveChangesAsync() > 0 ? string.Empty : "保存失败";
		}
	}
}
