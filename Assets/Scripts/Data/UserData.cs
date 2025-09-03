using UnityEngine;

namespace Data
{
	public class UserData
	{
		private int _energyValue;
		public int EnergyValue => _energyValue;

		private int _currencyValue;
		public int CurrencyValue => _currencyValue;

		private int _energyMaxValue;

		public void AddEnergy(int energyValue)
		{
			_energyValue += energyValue;
		}

		public void AddCurrency(int currencyValue)
		{
			_currencyValue += currencyValue;
		}
	}
}