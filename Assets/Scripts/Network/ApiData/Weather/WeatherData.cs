using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network.ApiData.Weather
{
	[Serializable]
	public class WeatherData
	{
		public WeatherProps properties;
	}

	[Serializable]
	public class WeatherProps
	{
		public List<WeatherPeriod> periods;
	}

	[Serializable]
	public class WeatherPeriod
	{
		public Sprite sprite;
		public string name;
		public string temperature;
		public string temperatureUnit;
		public string icon;
	}
}