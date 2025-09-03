using Configs;
using Managers;
using UnityEngine;
using Zenject;

namespace Contexts
{
	[CreateAssetMenu(fileName = "SO Initializer", menuName = "Installers/SOInitializer")]
	public class ScriptableObjectsInitializer : ScriptableObjectInstaller
	{
		[SerializeField] private WindowsConfig _windowsConfig;
		[SerializeField] private UserInitialConfig userInitialConfig;

		public override void InstallBindings()
		{
			Container.Bind<WindowsConfig>().FromInstance(_windowsConfig).AsSingle();
			Container.Bind<UserInitialConfig>().FromInstance(userInitialConfig).AsSingle();
		}
	}
}