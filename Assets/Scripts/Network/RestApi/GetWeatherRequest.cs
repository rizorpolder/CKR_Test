using Network.ApiData.Weather;

namespace Network.RestApi
{
	public class GetWeatherRequest : GetRequest<WeatherResponseBody>
	{
		protected override string Uri => "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

		protected override WeatherResponseBody ParseResponseData(byte[] data)
		{
			return new WeatherResponseBody().ParseFrom(data);
		}
	}
}