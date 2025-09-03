using System;
using DG.Tweening;
using UnityEngine;

namespace VFX
{
	public class CurrencyEffect : MonoBehaviour
	{
		public Action<CurrencyEffect> OnCompleted;

		[SerializeField] private CanvasGroup _canvasGroup;

		Sequence _tween;

		// beware of magic numbers (:

		private void Start()
		{
			_tween = DOTween.Sequence();
			_tween.Append(_canvasGroup.DOFade(1, 1.5f));
			_tween.Join(this.transform.DOMove(Vector3.up, 1.5f));
			_tween.Append(_canvasGroup.DOFade(0, 0f));
			_tween.OnComplete(() => OnCompleted?.Invoke(this));
		}

		public void Play()
		{
			_tween.Play();
		}

		public void Stop()
		{
			_tween?.Kill();
		}
	}
}