using System;

namespace Managers.Clicker
{
	public interface IClickerListener
	{
		event Action OnEnergyDataChanged;
		event Action<bool> OnCurrencyDataChanged;
	}
}