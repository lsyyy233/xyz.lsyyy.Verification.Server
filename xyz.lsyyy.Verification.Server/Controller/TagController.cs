using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xyz.lsyyy.Verification.Data;

namespace xyz.lsyyy.Verification.Controller
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class TagController : ControllerBase
	{
		private readonly MemoryActionTagService memTagService;
		private readonly ActionTagService tagService;

		public TagController(MemoryActionTagService memTagService, ActionTagService tagService)
		{
			this.memTagService = memTagService;
			this.tagService = tagService;
		}

		/// <summary>
		/// 添加Tag
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<object> AddTag([FromBody] ActionTagModel model)
		{
			if (string.IsNullOrWhiteSpace(model.ActionName))
			{
				return StatusCode(400, "ActionName不能为空");
			}
			if (string.IsNullOrWhiteSpace(model.ControllerName))
			{
				return StatusCode(400, "ControllerName不能为空");
			}
			if (string.IsNullOrWhiteSpace(model.Tag))
			{
				return StatusCode(400, "Tag不能为空");
			}

			string result = await tagService.AddTagAsync(model);
			if (string.IsNullOrWhiteSpace(result))
			{
				return Ok();
			}
			return BadRequest(result);
		}

		/// <summary>
		/// 获取Tag状态
		/// </summary>
		/// <returns></returns>
		[HttpGet("status")]
		public async Task<object> GetTagStatus()
		{
			IQueryable<ActionTag> actionTagsQuery = tagService.GetAllTag();
			List<ActionTag> actionTags = await actionTagsQuery.ToListAsync();
			IEnumerable<ActionTagModel> memActionTags = memTagService.GeMemTags();
			ActionTagStatus status = new ActionTagStatus
			{
				Normal =
					from at in actionTags
					join mat in memActionTags on at.TagName equals mat.Tag
					where mat.ActionName == at.ActionName && mat.ControllerName == at.ControllerName
					select at,
				Deleted =
					from at in actionTags
					where !memActionTags.Select(x => x.Tag).Contains(at.TagName)
					select at,
				New =
					from mat in memActionTags
					where !actionTags.Select(x => x.TagName).Contains(mat.Tag)
					select mat,
				Modified =
					from at in actionTags
					join mat in memActionTags on at.TagName equals mat.Tag
					where mat.ActionName != at.ActionName || mat.ControllerName != at.ControllerName
					select new ModifiedActionTag
					{
						New = mat,
						Old = at
					}
			};
			return Ok(status);
		}
	}
}
