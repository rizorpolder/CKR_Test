using System;

namespace Managers.Clicker
{
	public interface IClickerListener
	{
		event Action OnDataUpdated;
		event Action OnEnergyDataChanged;
		event Action OnCurrencyDataChanged;
		event Action<bool> OnStateChanged;

	}
}