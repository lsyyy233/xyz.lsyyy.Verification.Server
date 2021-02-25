using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification
{
	public class GrpcActionService : Protos.ActionService.ActionServiceBase
	{
		private readonly MemoryActionTagService _memoryActionTagService;
		public GrpcActionService(MemoryActionTagService memoryActionTagService)
		{
			this._memoryActionTagService = memoryActionTagService;
		}
		public override async Task<ActionResponse> PushActions(IAsyncStreamReader<ActionRequest> requestStream, ServerCallContext context)
		{
			List<ActionTagModel> actions = new List<ActionTagModel>();
			while (await requestStream.MoveNext())
			{
				ActionRequest request = requestStream.Current;
				actions.Add(new ActionTagModel
				{
					ActionName = request.ActionName,
					ControllerName = request.ControllerName,
					Tag = request.Tag
				});
			}
			_memoryActionTagService.FlushTagsInMemory(actions);
			return new ActionResponse
			{
				Status = true
			};
		}
	}
}
