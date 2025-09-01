using Zenject;

namespace Contexts
{
	public class Initializer : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<NetworkManager>().AsSingle();
		}
	}
}