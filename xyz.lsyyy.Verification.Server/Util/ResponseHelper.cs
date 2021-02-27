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
		public static GeneralResponse Response(bool isSuccess = true, string message = "")
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
	}
}
