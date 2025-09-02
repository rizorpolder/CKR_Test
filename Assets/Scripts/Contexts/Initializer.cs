using Managers;
using UnityEngine;
using Zenject;

namespace Contexts
{
	public class Initializer : MonoInstaller
	{
		[SerializeField] private WindowsController _windowsController;
		public override void InstallBindings()
		{
			Container.Bind<NetworkManager>().AsSingle();
			Container.Bind<WindowsController>().FromInstance(_windowsController).AsSingle();
		}
	}
}