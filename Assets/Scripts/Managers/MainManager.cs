using System;
using System.Collections.Generic;
using Managers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class MainManager : MonoBehaviour
	{
		[SerializeField] List<ViewWrapper> _views;

		private int _enabledView = 0;

		private void Start()
		{
			for (var index = 0; index < _views.Count; index++)
			{
				var view = _views[index];
				view.Initialize(index);
				view.OnButtonClicked += EnableView;
			}

			//_views[0].Enable();
		}

		private void EnableView(int index)
		{
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