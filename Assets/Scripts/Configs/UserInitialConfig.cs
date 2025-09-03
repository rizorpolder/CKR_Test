using System;
using UnityEngine;

namespace Configs
{

	[CreateAssetMenu(menuName = "Configs/User Initial Config", fileName = "UserInitialConfig")]
	public class UserInitialConfig : ScriptableObject
	{
		[SerializeField] EnergyInitialData _energyInitialData;
		[SerializeField] CurrencyInitialData _currencyInitialData;

		public EnergyInitialData GetEnergyInitialData() => _energyInitialData;
		public CurrencyInitialData GetCurrencyInitialData() => _currencyInitialData;
	}

	[Serializable]
	public class EnergyInitialData
	{
		[SerializeField] private int _startEnergyValue;
		[SerializeField] private int _maxEnergyValue;
		[SerializeField] private int _energyRestoreValue;
		[SerializeField] private int _energyRestoreTime;
	}

	[Serializable]
	public class CurrencyInitialData
	{
		[SerializeField] private int _startCurrencyValue;
		[SerializeField] private int _currencyPerClick;
		[SerializeField] private int _currencyAutocollectTime;
	}
}