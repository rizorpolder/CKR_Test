using Network.ApiData.Dogs;

namespace Network.RestApi
{
	public class GetPuppiesByIDRequest : GetRequest<PuppiesResponseBody>
	{
		private readonly string _id;
		protected override string Uri => $"https://dogapi.dog/api/v2/breeds/{_id}";

		public GetPuppiesByIDRequest(string id)
		{
			_id = id;
		}

		protected override PuppiesResponseBody ParseResponseData(byte[] data)
		{
			return new PuppiesResponseBody().ParseFrom(data);
		}
	}
}