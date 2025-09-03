using System;
using AudioManager.Runtime.Core.Manager;
using DG.Tweening;
using Managers.Clicker;
using UI.Clicker.UI.HUD;
using UnityEngine;
using UnityEngine.UI;
using VFX;
using Zenject;

namespace UI.Clicker.UI
{
	public class UIController : MonoBehaviour
	{
		[SerializeField] private AResourcePanel _energyPanel;
		[SerializeField] private AResourcePanel _currencyPanel;
		[SerializeField] private Button _collectButton;
		[SerializeField] private ParticleSystem _particles;
		[SerializeField] private CurrencyPool _currencyPool;

		[Inject] private ManagerAudio _managerAudio;
		[Inject] private IClickerListener _clickerListener;
		[Inject] private IClickerCommand _clickerCommand;
		[Inject] private IClickerData _clickerData;

		private Sequence _tween;

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

			if (!value)
			{
				_particles.Stop();
				_particles.Clear();
				_tween?.Kill();
				_currencyPool.Stop();
			}
		}

		private void OnEnergyDataChanged()
		{
			_energyPanel.SetResourceText(_clickerData.UserEnergy);
		}

		// beware of magic numbers (:
		private void OnCurrencyDataChanged()
		{
			_currencyPanel.SetResourceText(_clickerData.UserCurrency);
			_particles.Emit(10);
			PlayButtonAnimation();
			_managerAudio.PlayAudioClip(TAudio.coin.ToString());
			_currencyPool.Emit();
		}

		// beware of magic numbers (:
		private void PlayButtonAnimation()
		{
			_tween?.Kill();
			_tween = DOTween.Sequence();
			_tween.Append(_collectButton.transform.DOScale(1.1f, 0.1f));
			_tween.Join(_collectButton.image.DOColor(new Color(0.5f, 1f, 0.5f, 1f), 0.1f));
			_tween.Append(_collectButton.transform.DOScale(1.0f, 0.1f));
			_tween.Join(_collectButton.image.DOColor(Color.white, 0.1f));
			_tween.Play();
		}

		private void OnButtonClickHandler()
		{
			_clickerCommand.AddCurrency();
		}
	}
}