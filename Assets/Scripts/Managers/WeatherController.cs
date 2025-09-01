using System;
using Network.ApiData.Weather;
using Network.RestApi;
using UI.WeatherView;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class WeatherController : MonoBehaviour
	{
		[Inject] NetworkManager _networkManager;

		[SerializeField] private WeatherView _view;
		private GetWeatherRequest _weatherRequest;

		private void Start()
		{
			//TODO Подумать может на каждую вкладку создавать свое поведение

			_weatherRequest = new GetWeatherRequest();
			_weatherRequest.OnResponse += UpdateView;
			_networkManager.Add(_weatherRequest);
		}

		private void UpdateView(Response<WeatherRequestResponseBody> obj)
		{
			if (!obj.IsSuccess)
				return; // TODO if server down

			var periods = obj.Data.WeatherData.properties.periods;
			_view.Initialize(periods);
		}
	}
}