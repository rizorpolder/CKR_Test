using System;
using Network;
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

		private WeatherIconsCache _iconsCache;
		private GetWeatherRequest _weatherRequest;

		private void Start()
		{
			_iconsCache = new WeatherIconsCache();
			//TODO Подумать может на каждую вкладку создавать свое поведение

			_weatherRequest = new GetWeatherRequest();
			_weatherRequest.OnResponse += UpdateView;
			_networkManager.Add(_weatherRequest);
		}

		private async void UpdateView(Response<WeatherRequestResponseBody> obj)
		{
			if (!obj.IsSuccess)
				return; // TODO if server down

			var periods = obj.Data.WeatherData.properties.periods;

			foreach (var period in periods)
			{
				_iconsCache.ParseUrl(period.icon, out WeatherIconsCache.WeatherNode node);

				if (!node.SpriteIcon)
				{
					var request = new GetTextureRequest(period.icon);
					var result = await request.Make();
					node.SetSprite(result);
				}

				period.sprite = node.SpriteIcon;
			}

			_view.Initialize(periods);
		}
	}
}