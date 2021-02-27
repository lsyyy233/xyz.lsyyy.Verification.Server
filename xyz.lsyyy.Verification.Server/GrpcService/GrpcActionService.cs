using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification
{
	public class GrpcActionService : ActionRpcService.ActionRpcServiceBase
	{
		private readonly MemoryActionTagService _memoryActionTagService;
		public GrpcActionService(MemoryActionTagService memoryActionTagService)
		{
			this._memoryActionTagService = memoryActionTagService;
		}

		/// <summary>
		/// 推送Tag
		/// </summary>
		/// <param name="requestStream"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task<PushActionResponse> PushActionTag(IAsyncStreamReader<TagInfo> requestStream, ServerCallContext context)
		{
			List<ActionTagModel> actions = new List<ActionTagModel>();
			while (await requestStream.MoveNext())
			{
				TagInfo request = requestStream.Current;
				actions.Add(new ActionTagModel
				{
					ActionName = request.ActionName,
					ControllerName = request.ControllerName,
					Tag = request.TagName
				});
			}
			_memoryActionTagService.FlushTagsInMemory(actions);
			return new PushActionResponse
			{
				Status = true
			};
		}
	}
}
