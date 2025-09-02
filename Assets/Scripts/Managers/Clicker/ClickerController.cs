using Managers.Base;

namespace Managers.Clicker
{
	public class ClickerController : ABaseController
	{
		public override void Enable()
		{
			//todo load data or use from config
		}

		public override void Disable()
		{
			//save data and stop service
		}
	}
}