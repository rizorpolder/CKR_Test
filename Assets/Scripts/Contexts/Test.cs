using Network.ApiData.Dogs;
using Network.ApiData.Weather;
using Network.RestApi;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Contexts
{
	public class Test : MonoBehaviour
	{
		[SerializeField] private Button _button;

		[Inject]
		private NetworkManager _networkManager;

		private GetPuppiesListRequest _request;

		private void Start()
		{
			_button.onClick.AddListener(OnButtonClickHandler);
			_request = new GetPuppiesListRequest();
			_request.OnResponse += UpdateView;
		}

		private void UpdateView(Response<PuppiesResponseBody> obj)
		{
			var temp = obj.Data.PuppiesData;
			//получили данные
			// нужно запросить все картинки по ссылкам и создать "словарь", чтоб не грузить все картинки по 10 раз
		}

		private void OnButtonClickHandler()
		{
			_networkManager.Add(_request);
		}
	}
}