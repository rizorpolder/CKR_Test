using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class WindowsController : MonoBehaviour
	{
		[Inject] WindowsConfig _windowsConfig;
		[Inject] DiContainer _container;

		[SerializeField] private Transform parent;

		private List<WindowInstance> _activeWindows = new List<WindowInstance>();

		private WindowsFactory _factory;

		private void Start()
		{
			_factory = new WindowsFactory(_container);
			_factory.Initialize(_windowsConfig);
		}

		public void Show(WindowType windowType, Action<BaseWindow> callback = null)
		{
			if (!_factory.GetWindow(windowType, out var window))
			{
				Debug.Log($"No window found for window type {windowType}");
				return;
			}

			Show(window, callback);
		}

		public void Show(string windowName, Action<BaseWindow> callback = null)
		{
			if (!_factory.GetWindow(windowName, out var window))
			{
				Debug.Log($"No window found for window type {windowName}");
				return;
			}

			Show(window, callback);
		}

		private void Show(WindowInstance window, Action<BaseWindow> callback = null)
		{
			_factory.CreateWindow(window,
				parent,
				() =>
				{
					OnShowAction(window);

					callback?.Invoke(window.Window);
				});
		}

		private void OnShowAction(WindowInstance windowInstance)
		{
			windowInstance.Window.Show();
			_activeWindows.Add(windowInstance);
		}

		public void Hide(WindowType windowType)
		{
			var window = _activeWindows.Find(x => x.Properties.type == windowType);
			HideWindow(window);
		}

		public void Hide(string windowName)
		{
			var window = _activeWindows.Find(x => x.Properties.name == windowName);
			HideWindow(window);
		}

		private void HideWindow(WindowInstance window)
		{
			if (window == null || window.Window == null)
				return;


			Hide(window);
		}

		private void Hide(WindowInstance window)
		{
			if (window == null || window.Window == null)
				return;

			window.Window.Hide(() => { _factory.DestroyWindow(window); });
		}
	}
}