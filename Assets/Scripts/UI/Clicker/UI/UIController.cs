using System;
using Managers.Clicker;
using UI.Clicker.UI.HUD;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Clicker.UI
{
	public class UIController : MonoBehaviour
	{
		[SerializeField] private AResourcePanel _energyPanel;
		[SerializeField] private AResourcePanel _currencyPanel;
		[SerializeField] private Button _collectButton;

		[Inject] private IClickerListener _clickerListener;
		[Inject] private IClickerCommand _clickerCommand;
		[Inject] private IClickerData _clickerData;

		private void Start()
		{
			_collectButton.onClick.AddListener(OnButtonClickHandler);
			_clickerListener.OnDataUpdated += UpdateView;
			_clickerListener.OnCurrencyDataChanged += OnCurrencyDataChanged;
			_clickerListener.OnEnergyDataChanged += OnEnergyDataChanged;
			_clickerListener.OnStateChanged += OnViewStateChanged;
		}

		private void UpdateView()
		{
			_energyPanel.SetResourceText(_clickerData.UserEnergy);
			_currencyPanel.SetResourceText(_clickerData.UserCurrency);
		}

		private void OnViewStateChanged(bool value)
		{
			gameObject.SetActive(value);
		}

		private void OnEnergyDataChanged()
		{
			_energyPanel.SetResourceText(_clickerData.UserEnergy);
		}

		private void OnCurrencyDataChanged()
		{
			_currencyPanel.SetResourceText(_clickerData.UserCurrency);
			//TODO Create VFX and DoTween item;
		}

		private void OnButtonClickHandler()
		{
			_clickerCommand.AddCurrency();
		}
	}
}