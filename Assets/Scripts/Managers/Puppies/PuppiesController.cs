using Managers.Base;
using Network.ApiData.Dogs;
using Network.RestApi;
using UI.PuppiesView;
using UI.Windows;
using UnityEngine;
using Zenject;

namespace Managers.Puppies
{
	public class PuppiesController : ABaseController
	{
		[Inject] NetworkManager _networkManager;
		[Inject] WindowsController _windowsController;
		[SerializeField] private PuppiesView _view;

		private GetPuppiesListRequest _dataListRequest;

		private GetPuppiesByIDRequest _lastCalledInfoReq = null;

		private void Start()
		{
			_view.Initialize();
			_view.OnEntryClicked += OnEntryClicked;
			_dataListRequest = new GetPuppiesListRequest();
			_dataListRequest.OnResponse += ListDataReceived;
		}

		private void ListDataReceived(Response<PuppiesListResponseBody> data)
		{
			_view.UpdateView(data.Data.PuppiesDataList);
		}

		private void OnEntryClicked(string id)
		{
			if (_lastCalledInfoReq != null)
			{
				_networkManager.Remove(_lastCalledInfoReq);
				_lastCalledInfoReq = null;
			}

			RequestSingleData(id);
		}

		private void RequestSingleData(string id)
		{
			_lastCalledInfoReq = new GetPuppiesByIDRequest(id);
			_networkManager.Add(_lastCalledInfoReq);
			_lastCalledInfoReq.OnResponse += SingleDataReceived;
		}

		private void SingleDataReceived(Response<PuppiesResponseBody> obj)
		{
			_lastCalledInfoReq.OnResponse -= SingleDataReceived;
			_lastCalledInfoReq = null;
			_windowsController.Show(WindowType.InfoPupup,
				window =>
				{
					if (window is not InfoPopup infoPopup)
						return;

					var itemName = obj.Data.PuppiesData.data.attributes.name;
					var itemDescr = obj.Data.PuppiesData.data.attributes.description;
					infoPopup.Initialize(itemName, itemDescr);
				});
		}

		public override void Enable()
		{
			_view.gameObject.SetActive(true);
			_networkManager.Add(_dataListRequest);
		}

		public override void Disable()
		{
			_view.gameObject.SetActive(false);
			if (_lastCalledInfoReq != null)
			{
				_networkManager?.Remove(_lastCalledInfoReq);
			}

			_view.ResetState();
		}
	}
}