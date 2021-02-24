using System;
using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Services
{
	public class ActionService : Protos.ActionService.ActionServiceBase
	{
		private readonly List<ActionTagMap> actions = new List<ActionTagMap>();
		public override async Task<ActionResponse> PushActions(IAsyncStreamReader<ActionRequest> requestStream, ServerCallContext context)
		{
			while (await requestStream.MoveNext())
			{
				ActionRequest request = requestStream.Current;
				//actions.Add(new ActionTagMap
				//{
				//	ActionName = request.ActionName,
				//	ControllerName = request.ControllerName,
				//	Tag = request.Tag
				//});
				Console.WriteLine($"ActionName = {request.ActionName} | ControllerName = {request.ControllerName} | Tag = {request.Tag}");
			}
			return new ActionResponse
			{
				Status = true
			};
		}
	}

	public class ActionTagMap
	{
		public string ActionName { get; set; }

		public string ControllerName { get; set; }

		public string Tag { get; set; }
	}
}
