using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	public abstract class ObjectsPool<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField] private Transform _poolRoot;
		[SerializeField] private T prefab;
		[SerializeField] private int _initializeItemsCount;
		[SerializeField] private List<T> _items;

		private readonly Queue<T> _freeElements = new();

		public T Prefab => prefab;
		public int ActiveItems => _items.Count - _freeElements.Count;

		public void InitializePool()
		{
			ResetPool();

			for (var i = 0; i < _initializeItemsCount; i++)
			{
				var item = CreateItem();
				_items.Add(item);
				_freeElements.Enqueue(item);
			}
		}

		public void InitializePool(int count)
		{
			_initializeItemsCount = count;
			InitializePool();
		}

		private T CreateItem()
		{
			var item = Instantiate(prefab, _poolRoot);
			item.gameObject.SetActive(false);
			return item;
		}

		public T GetItem()
		{
			if (!_freeElements.TryDequeue(out var item))
			{
				item = CreateItem();
				_items.Add(item);
			}

			item.gameObject.SetActive(true);
			return item;
		}

		public List<T> GetActiveItems()
		{
			return _items;
		}

		public void ReturnToPool(T item)
		{
			_freeElements.Enqueue(item);
			item.gameObject.SetActive(false);
		}

		public void ResetPool()
		{
			_freeElements.Clear();

			foreach (var item in _items) ReturnToPool(item);
		}
	}
}