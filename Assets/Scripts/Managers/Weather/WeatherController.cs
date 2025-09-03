using System;
using Common;
using Managers.Base;
using Network;
using Network.ApiData.Weather;
using Network.RestApi;
using UI.WeatherView;
using UniRx;
using UnityEngine;
using Zenject;

namespace Managers.Weather
{
	public class WeatherController : ABaseController
	{
		private const int TIMER_DELAY = 5;

		[Inject] private NetworkManager _networkManager;
		[SerializeField] private WeatherView _view;

		private WeatherIconsCache _iconsCache;
		private GetWeatherRequest _weatherRequest;

		private IDisposable _timer;

		private void Start()
		{
			_view.Initialize();
			_iconsCache = new WeatherIconsCache();
			_weatherRequest = new GetWeatherRequest();
			_weatherRequest.OnResponse += UpdateView;
		}

		private void StartTimer()
		{
			_timer = Observable.Interval(TimeSpan.FromSeconds(TIMER_DELAY))
				.Subscribe(
					l => { SendRequest(); },
					onCompleted: () =>
					{
						_timer.Dispose();
						StartTimer();
					});
		}

		private void SendRequest()
		{
			_networkManager.Add(_weatherRequest);
		}

		private async void UpdateView(Response<WeatherResponseBody> obj)
		{
			if (!obj.IsSuccess)
				return; // TODO if server down

			var periods = obj.Data.WeatherData.properties.periods;

			foreach (var period in periods)
			{
				_iconsCache.ParseUrl(period.icon, out var node);

				if (!node.SpriteIcon)
				{
					var request = new GetTextureRequest(period.icon);
					var result = await request.Make();
					node.SetSprite(result);
				}

				period.sprite = node.SpriteIcon;
			}

			_view.UpdateView(periods);
		}

		public override void Enable()
		{
			_view.gameObject.SetActive(true);
			SendRequest();
			StartTimer();
		}

		private void Clear()
		{
			_view?.gameObject.SetActive(false);
			_timer?.Dispose();
			_networkManager?.Remove(_weatherRequest);
			_view?.ResetState();
		}

		public override void Disable()
		{
			Clear();
		}
	}
}