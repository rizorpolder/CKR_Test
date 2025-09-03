using Common;
using UnityEngine;

namespace VFX
{
	public class CurrencyPool : ObjectsPool<CurrencyEffect>
	{
		[SerializeField] private Transform _parent;

		private void Start()
		{
			InitializePool();
		}


		public void Emit(int currencyValue)
		{
			var element = GetItem();
			element.Initialize(currencyValue);
			element.transform.position = _parent.position;
			element.OnCompleted += OnAnimationCompleted;
			element.Play();
		}

		private void OnAnimationCompleted(CurrencyEffect obj)
		{
			obj.OnCompleted -= OnAnimationCompleted;
			ReturnToPool(obj);
		}

		public void Stop()
		{
			var items = GetActiveItems();
			foreach (var effect in items)
			{
				effect.Stop();
				ReturnToPool(effect);
			}
		}
	}
}