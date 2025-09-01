using Network.ApiData.Weather;

namespace Network.RestApi
{
	public class GetWeatherRequest : GetRequest<WeatherRequestResponseBody>
	{
		protected override string Uri => "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

		protected override WeatherRequestResponseBody ParseResponseData(byte[] data)
		{
			return new WeatherRequestResponseBody().ParseFrom(data);
		}
	}
}