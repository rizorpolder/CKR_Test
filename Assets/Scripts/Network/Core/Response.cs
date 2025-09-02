namespace Network.RestApi
{
	public class Response<TResponseData>
	{
		public TResponseData Data;
		public bool IsSuccess;

		private Response()
		{
			IsSuccess = false;
		}

		private Response(TResponseData data)
		{
			IsSuccess = true;
			Data = data;
		}

		public static Response<TResponseData> Failed()
		{
			return new Response<TResponseData>();
		}

		public static Response<TResponseData> Success(TResponseData data)
		{
			return new Response<TResponseData>(data);
		}
	}
}