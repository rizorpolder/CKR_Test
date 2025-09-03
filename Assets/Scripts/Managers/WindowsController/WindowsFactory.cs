using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class WindowsFactory
	{
		private List<WindowInstance> _instances = new List<WindowInstance>();

		private DiContainer _container;

		public WindowsFactory(DiContainer container)
		{
			_container = container;
		}

		public void Initialize(WindowsConfig config)
		{
			foreach (var configWindow in config.windows)
			{
				_instances.Add(new WindowInstance(configWindow));
			}
		}

		public bool GetWindow(string name, out WindowInstance windowInstance)
		{
			windowInstance = _instances.Find(x => x.Properties.name.Equals(name));
			return windowInstance != null;
		}

		public bool GetWindow(WindowType type, out WindowInstance windowInstance)
		{
			windowInstance = _instances.Find(x => x.Properties.type.Equals(type));
			return windowInstance != null;
		}

		public void CreateWindow(WindowInstance window, Transform parent, Action callback)
		{
			// если есть закешированное окно
			if (window.Window != null)
			{
				window.Window.transform.SetParent(parent);
				callback?.Invoke();
				return;
			}

			AsyncInstantiateOperation<GameObject> operation =
				GameObject.InstantiateAsync(window.Properties.assetReference, parent);
			operation.completed += _ =>
			{
				window.Window = operation.Result[0].GetComponent<BaseWindow>();
				window.Window.transform.localPosition = Vector3.zero;
				_container.Inject(window.Window);
				callback?.Invoke();
			};
		}

		public void DestroyWindow(WindowInstance window)
		{
			if (!window.Properties.IsCached)
			{
				window.Destroy();
			}
		}
	}
}