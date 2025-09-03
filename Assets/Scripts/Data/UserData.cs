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

		public UserData(int currencyValue, int energyValue)
		{
			_energyValue = energyValue;
			_currencyValue = currencyValue;
		}

		public void SetEnergy(int energyValue)
		{
			_energyValue = energyValue;
		}

		public void AddCurrency(int currencyValue)
		{
			_currencyValue += currencyValue;
		}
	}
}