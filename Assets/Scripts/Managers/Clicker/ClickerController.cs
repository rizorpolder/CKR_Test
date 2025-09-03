using System;
using Common;
using Configs;
using Data;
using Managers.Base;
using UniRx;
using UnityEngine;
using Zenject;

namespace Managers.Clicker
{
	public class ClickerController : ABaseController, IClickerData, IClickerCommand, IClickerListener
	{
		private const string COLLECT_KEY = "autoCollectTimer";
		private const string ENERGY_KEY = "energyTimer";

		public event Action OnDataUpdated;
		public event Action OnEnergyDataChanged;
		public event Action OnCurrencyDataChanged;
		public event Action<bool> OnStateChanged;

		[Inject] private UserInitialConfig _userInitialConfig;

		public int UserCurrency => _userData.CurrencyValue;
		public int UserEnergy => _userData.EnergyValue;
		public int CurrencyReward => _currencyData.CurrencyPerClick;

		private UserData _userData;
		private EnergyInitialData _energyData;
		private CurrencyInitialData _currencyData;

		private bool _isInitialized = false;

		private IDisposable _autoCollectTimer;
		private IDisposable _energyTimer;

		public override void Enable()
		{
			OnStateChanged?.Invoke(true);
			InitializeUserData();
			StartEnergyTimer();
			StartCollectTimer();
		}

		private void InitializeUserData()
		{
			if (!_isInitialized)
			{
				_energyData = _userInitialConfig.GetEnergyInitialData();
				_currencyData = _userInitialConfig.GetCurrencyInitialData();
				_isInitialized = true;
			}

			_userData = new UserData(_currencyData.StartCurrencyValue, _energyData.StartEnergyValue);
			OnDataUpdated?.Invoke();
		}

		public void AddEnergy(int energyValue) // +10 - 1
		{
			var result = Mathf.Clamp(_userData.EnergyValue + energyValue, 0, _energyData.MaxEnergyValue);

			_userData.SetEnergy(result);
			OnEnergyDataChanged?.Invoke();
		}

		public void AddCurrency()
		{
			RestartTimer();

			if (_userData.EnergyValue <= 0)
				return;

			_userData.AddCurrency(_currencyData.CurrencyPerClick);
			AddEnergy(-_energyData.EnergyClickPrice);
			OnCurrencyDataChanged?.Invoke();
		}

		private void StartCollectTimer()
		{
			_autoCollectTimer = Observable.Timer(TimeSpan.FromSeconds(_currencyData.CurrencyAutocollectTime)).Subscribe(
				(_) => { },
				onCompleted: AddCurrency);
		}

		private void RestartTimer()
		{
			_autoCollectTimer?.Dispose();
			StartCollectTimer();
		}

		private void StartEnergyTimer()
		{
			_energyTimer = Observable.Timer(TimeSpan.FromSeconds(_energyData.EnergyRestoreTime)).Subscribe(
				(_) => { },
				onCompleted: () =>
				{
					AddEnergy(_energyData.EnergyRestoreValue);
					RestartEnergyTimer();
				});
		}

		private void RestartEnergyTimer()
		{
			_energyTimer.Dispose();
			StartEnergyTimer();
		}

		public override void Disable()
		{
			_energyTimer?.Dispose();
			_autoCollectTimer?.Dispose();

			OnStateChanged?.Invoke(false);
		}
	}
}