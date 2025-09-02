using Network.ApiData.Dogs;
using Network.RestApi;

public class GetPuppiesListRequest : GetRequest<PuppiesResponseBody>
{
	protected override string Uri => "https://dogapi.dog/api/v2/breeds";

	protected override PuppiesResponseBody ParseResponseData(byte[] data)
	{
		return new PuppiesResponseBody().ParseFrom(data);
	}
}