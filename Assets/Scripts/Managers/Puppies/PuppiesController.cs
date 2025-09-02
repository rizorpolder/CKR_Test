using Managers.Base;
using UI.PuppiesView;
using UnityEngine;
using Zenject;

namespace Managers.Puppies
{
	public class PuppiesController : ABaseController
	{
		[Inject] NetworkManager _networkManager;

		[SerializeField] private PuppiesView _view;

		//todo remove last called info
		// hide window
		private void Start()
		{
		}

		public override void Enable()
		{
			_view.gameObject.SetActive(true);
		}

		public override void Disable()
		{
			_view.gameObject.SetActive(false);
		}
	}
}