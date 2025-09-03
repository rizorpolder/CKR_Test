using System;
using System.Collections.Generic;
using AudioManager.Runtime.Core.Manager;
using Managers.Base;
using Managers.Clicker;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
	public class MainManager : MonoBehaviour
	{
		[SerializeField] List<ViewWrapper> _views;
		[Inject] private ManagerAudio _managerAudio;
		private int _enabledView = 0;

		private void Start()
		{
			for (var index = 0; index < _views.Count; index++)
			{
				var view = _views[index];
				view.Initialize(index);
				view.OnButtonClicked += EnableView;
			}

			_views[_enabledView].Enable();
		}

		private void EnableView(int index)
		{
			_managerAudio.PlayAudioClip(TAudio.click.ToString());


			if (_enabledView == index)
				return;

			_views[_enabledView].Disable();
			_enabledView = index;
			_views[index].Enable();
		}

		private void OnDestroy()
		{
			foreach (var view in _views)
			{
				view.OnButtonClicked -= EnableView;
			}
		}
	}

	[Serializable]
	public class ViewWrapper
	{
		public Action<int> OnButtonClicked;

		[SerializeField] public Button _button;
		[SerializeField] public ABaseController _controller;

		private int _index = 0;

		public void Initialize(int index)
		{
			_index = index;
			_button.onClick.AddListener(OnButtonClickHandler);
		}

		private void OnButtonClickHandler()
		{
			OnButtonClicked?.Invoke(_index);
		}

		public void Enable() => _controller.Enable();

		public void Disable() => _controller.Disable();
	}
}