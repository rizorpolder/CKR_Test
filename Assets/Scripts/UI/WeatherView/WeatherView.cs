using System.Collections.Generic;
using Common;
using Data;
using UnityEngine;

namespace UI.WeatherView
{
	public class WeatherView : ObjectsPool<WeatherItem>
	{
		[SerializeField] private GameObject _loadingView;

		private void Start()
		{
			InitializePool();
		}

		public void UpdateView(List<WeatherPeriod> weatherPeriods)
		{
			ResetPool();
			foreach (var period in weatherPeriods)
			{
				var item = GetItem();
				item.SetIcon(period.sprite)
					.SetDescription(period.name, period.temperature, period.temperatureUnit);
			}

			if (_loadingView.activeSelf)
				_loadingView.SetActive(false);
		}
	}
}