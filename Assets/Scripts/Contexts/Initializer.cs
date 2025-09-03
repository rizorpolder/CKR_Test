using Common;
using Managers;
using Managers.Clicker;
using UnityEngine;
using Zenject;

namespace Contexts
{
	public class Initializer : MonoInstaller
	{
		[SerializeField] private WindowsController _windowsController;
		[SerializeField] private ClickerController _clickerController;

		public override void InstallBindings()
		{
			Container.Bind<NetworkManager>().AsSingle();
			Container.Bind<CooldownManager>().AsSingle();
			Container.Bind<IClickerListener>().To<ClickerController>().FromInstance(_clickerController);
			Container.Bind<IClickerCommand>().To<ClickerController>().FromInstance(_clickerController);
			Container.Bind<IClickerData>().To<ClickerController>().FromInstance(_clickerController);
			Container.Bind<WindowsController>().FromInstance(_windowsController).AsSingle();
		}
	}
}