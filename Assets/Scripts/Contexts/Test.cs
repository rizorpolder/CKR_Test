using System;
using Data;
using Network.ApiData.Weather;
using Network.RestApi;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Contexts
{
	public class Test : MonoBehaviour
	{
		[Inject]
		private NetworkManager _networkManager;

		[SerializeField] private Button _button;

		GetWeatherRequest _request;

		private void Start()
		{
			_button.onClick.AddListener(OnButtonClickHandler);
			_request = new GetWeatherRequest();
			_request.OnResponse += UpdateView;
		}

		private void UpdateView(Response<WeatherRequestResponseBody> obj)
		{
			var temp = obj.Data.WeatherData;
			//получили данные
			// нужно запросить все картинки по ссылкам и создать "словарь", чтоб не грузить все картинки по 10 раз
		}

		private void OnButtonClickHandler()
		{
			_networkManager.Add(_request);
		}

	}
}