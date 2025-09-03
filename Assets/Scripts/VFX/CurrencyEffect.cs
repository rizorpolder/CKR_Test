using System;
using DG.Tweening;
using UnityEngine;

namespace VFX
{
	public class CurrencyEffect : MonoBehaviour
	{
		public Action<CurrencyEffect> OnCompleted;

		[SerializeField] private CanvasGroup _canvasGroup;

		private Sequence _tween;

		// beware of magic numbers (:

		public void Play()
		{
			_tween = DOTween.Sequence();
			_tween.Append(_canvasGroup.DOFade(1, 1.5f));
			_tween.Join(this.transform.DOMove(Vector3.up, 1.5f));
			_tween.Append(_canvasGroup.DOFade(0, 0f));
			_tween.OnComplete(() =>
			{
				OnCompleted?.Invoke(this);
			}).Play();
		}

		public void Stop()
		{
			_tween?.Kill();
		}
	}
}