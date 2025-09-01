using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	public abstract class ObjectsPool<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField] private Transform _poolRoot;
		[SerializeField] private T prefab;
		[SerializeField] private int _initializeItemsCount = 0;
		[SerializeField] private List<T> _items;

		public T Prefab => prefab;

		private Queue<T> _freeElements = new Queue<T>();
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
			var item = Instantiate<T>(prefab, _poolRoot);
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

			foreach (var item in _items)
			{
				ReturnToPool(item);
			}
		}
	}
}