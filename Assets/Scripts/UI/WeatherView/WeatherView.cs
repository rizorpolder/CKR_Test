using System.Collections.Generic;
using Common;
using Data;

namespace UI.WeatherView
{
	public class WeatherView : ObjectsPool<WeatherItem>
	{
		public void Initialize(List<WeatherPeriod> weatherPeriods)
		{
			InitializePool();

			foreach (var period in weatherPeriods)
			{
				var item = GetItem();
				item.SetIcon(period.sprite)
					.SetDescription(period.name, period.temperature, period.temperatureUnit);
			}
		}
	}
}