using System;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Util
{
	public static class ResponseHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="isSuccess"></param>
		/// <returns></returns>
		private static GeneralResponse Response(bool isSuccess, string message)
		{
			if (isSuccess)
			{
				message = "未知错误";
			}
			return new GeneralResponse
			{
				IsSuccess = isSuccess,
				Message = message
			};
		}

		public static GeneralResponse OkResponse()
		{
			return Response(true,String.Empty);
		}

		public static GeneralResponse BadResponse(string message = "未知错误")
		{
			return Response(false, message);
		}

	}
}
