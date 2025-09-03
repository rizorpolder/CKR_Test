using System;
using Configs;
using Data;
using Managers.Base;
using Managers.SaveDataManager;
using Zenject;

namespace Managers.Clicker
{
	public class ClickerController : ABaseController, IClickerData,IClickerCommand, IClickerListener
	{
		public event Action OnUserDataChanged;

		[Inject] ADataSaver _dataSaver;
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

		}

		public override void Disable()
		{
			//save data and stop service
		}


		public void AddEnergy(int energyValue)
		{
			throw new NotImplementedException();
		}

		public void AddCurrency(int currencyValue)
		{
			throw new NotImplementedException();
		}
	}
}