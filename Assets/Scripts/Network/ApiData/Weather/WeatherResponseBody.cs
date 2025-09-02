using System.Text;
using Data;
using Newtonsoft.Json;

namespace Network.ApiData.Weather
{
	public class WeatherResponseBody : IParse<WeatherResponseBody>
	{
		public WeatherData WeatherData;

		public WeatherResponseBody ParseFrom(byte[] data)
		{
			var str = Encoding.UTF8.GetString(data);
			var result = JsonConvert.DeserializeObject<WeatherData>(str);
			return new WeatherResponseBody {WeatherData = result};
		}
	}
}