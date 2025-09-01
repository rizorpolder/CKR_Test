using System.Text;
using Data;

namespace Network.ApiData.Weather
{
	public class WeatherRequestResponseBody : IParse<WeatherRequestResponseBody>
	{
		public WeatherData WeatherData;

		public WeatherRequestResponseBody ParseFrom(byte[] data)
		{
			var str = Encoding.UTF8.GetString(data);
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherData>(str);
			return new WeatherRequestResponseBody() {WeatherData = result};
		}
	}
}