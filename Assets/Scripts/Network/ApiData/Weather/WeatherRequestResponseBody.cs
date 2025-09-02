using System.Text;
using Data;
using Newtonsoft.Json;

namespace Network.ApiData.Weather
{
	public class WeatherRequestResponseBody : IParse<WeatherRequestResponseBody>
	{
		public WeatherData WeatherData;

		public WeatherRequestResponseBody ParseFrom(byte[] data)
		{
			var str = Encoding.UTF8.GetString(data);
			var result = JsonConvert.DeserializeObject<WeatherData>(str);
			return new WeatherRequestResponseBody {WeatherData = result};
		}
	}
}