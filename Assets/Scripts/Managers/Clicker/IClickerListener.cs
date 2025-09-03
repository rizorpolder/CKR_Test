using System;

namespace Managers.Clicker
{
	public interface IClickerListener
	{
		event Action OnUserDataChanged;
	}
}