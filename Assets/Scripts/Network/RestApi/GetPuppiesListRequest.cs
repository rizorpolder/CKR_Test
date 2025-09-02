using Network.ApiData.Dogs;

namespace Network.RestApi
{
	public class GetPuppiesListRequest : GetRequest<PuppiesListResponseBody>
	{
		protected override string Uri => "https://dogapi.dog/api/v2/breeds";

		protected override PuppiesListResponseBody ParseResponseData(byte[] data)
		{
			return new PuppiesListResponseBody().ParseFrom(data);
		}
	}
}