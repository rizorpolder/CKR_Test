using System.Text;
using Data;
using Newtonsoft.Json;

namespace Network.ApiData.Dogs
{
	public class PuppiesListResponseBody : IParse<PuppiesListResponseBody>
	{
		public PuppiesDataList PuppiesDataList;

		public PuppiesListResponseBody ParseFrom(byte[] data)
		{
			var str = Encoding.UTF8.GetString(data);
			var result = JsonConvert.DeserializeObject<PuppiesDataList>(str);
			return new PuppiesListResponseBody {PuppiesDataList = result};
		}
	}

	public class PuppiesResponseBody : IParse<PuppiesResponseBody>
	{
		public PuppiesData PuppiesData;

		public PuppiesResponseBody ParseFrom(byte[] data)
		{
			var str = Encoding.UTF8.GetString(data);
			var result = JsonConvert.DeserializeObject<PuppiesData>(str);
			return new PuppiesResponseBody {PuppiesData = result};
		}
	}
}