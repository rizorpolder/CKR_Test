using System;
using Common;
using Configs;
using Data;
using Managers.Base;
using Managers.SaveDataManager;
using UnityEngine;
using Zenject;

namespace Managers.Clicker
{
	public class ClickerController : ABaseController, IClickerData, IClickerCommand, IClickerListener
	{
		private const string COLLECT_KEY = "autoCollectTimer";
		private const string ENERGY_KEY = "energyTimer";
		public event Action OnEnergyDataChanged;
		public event Action<bool> OnCurrencyDataChanged;

		[Inject] ADataSaver _dataSaver;
		[Inject] private CooldownManager _cooldownManager;

		[Inject] UserInitialConfig _userInitialConfig;

		private UserData _userData;
		public int UserCurrency => _userData.CurrencyValue;
		public int UserEnergy => _userData.EnergyValue;

		public override void Enable()
		{
			var userData = _dataSaver.LoadData<UserData>("UserData");
			if (userData is null)
			{
				userData = new UserData();
			}

			StartEnergyTimer();
		}

		public void AddEnergy(int energyValue)
		{
			_userData.AddEnergy(energyValue);
			OnEnergyDataChanged?.Invoke();
		}

		private void SetCurrencyValue()
		{
			OnCurrencyDataChanged?.Invoke(false);
		}

		public void AddCurrency()
		{
			_userData.AddCurrency(1); //TODO From COnfig
			RestartTimer();
			OnCurrencyDataChanged?.Invoke(true);
		}

		private void RestartTimer()
		{
			var timer = _cooldownManager.GetCooldown(COLLECT_KEY);
			if (!timer.IsComplete)
			{
				_cooldownManager.RemoveCooldown(COLLECT_KEY);
			}

			StartCollectTimer();
		}

		private void StartCollectTimer()
		{
			var timer = _cooldownManager.SetCooldown(COLLECT_KEY, TimeSpan.FromSeconds(3)); //TODO FromConfig
			timer.Completed += c =>
			{
				StartCollectTimer();
				Debug.Log("Auto collect");
			};
		}

		private void StartEnergyTimer()
		{
			var timer = _cooldownManager.SetCooldown(ENERGY_KEY, TimeSpan.FromSeconds(10)); //TODO From Config
			timer.Completed += c =>
			{
				AddEnergy(10); //todo from config
				StartEnergyTimer();
			};
		}

		public override void Disable()
		{
			_cooldownManager.RemoveCooldown(COLLECT_KEY);
			_cooldownManager.RemoveCooldown(ENERGY_KEY);
			//TODO Save data
		}
	}
}