using Managers;
using UnityEngine;
using Zenject;

namespace Contexts
{
	[CreateAssetMenu(fileName = "SO Initializer", menuName = "Installers/SOInitializer")]
	public class ScriptableObjectsInitializer : ScriptableObjectInstaller
	{
		[SerializeField] private WindowsConfig _windowsConfig;

		public override void InstallBindings()
		{
			Container.Bind<WindowsConfig>().FromInstance(_windowsConfig).AsSingle();
		}
	}
}