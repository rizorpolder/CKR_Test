using System;
using Common;
using Data;
using UnityEngine;

namespace UI.PuppiesView
{
	public class PuppiesView : ObjectsPool<PuppiesEntryView>
	{
		private const int MAX_ENTRIES = 10;

		public Action<string> OnEntryClicked;

		[SerializeField] private GameObject _loadingView;

		private PuppiesEntryView _pendingEntry = null;

		public void Initialize()
		{
			InitializePool();
		}

		public void UpdateView(PuppiesDataList data)
		{
			ResetPool();
			var dataCount = Math.Clamp(data.data.Count, data.data.Count, MAX_ENTRIES);
			for (var index = 0; index < dataCount; index++)
			{
				var entry = data.data[index];
				var item = GetItem();
				item.Initialize(index + 1, entry);
				item.OnEntryClicked += OnEntryItemCalled;
			}

			if (_loadingView.activeSelf)
				_loadingView.SetActive(false);
		}

		private void OnEntryItemCalled(PuppiesEntryView entry)
		{
			if (_pendingEntry)
			{
				StopLoading();
			}

			_pendingEntry = entry;
			OnEntryClicked?.Invoke(entry.ItemID);
		}

		private void UnsubscribeEvents()
		{
			var items = this.GetActiveItems();
			foreach (var puppiesEntry in items)
			{
				puppiesEntry.OnEntryClicked -= OnEntryItemCalled;
			}
		}

		public void ResetState()
		{
			UnsubscribeEvents();
			ResetPool();
			_loadingView.SetActive(true);
		}

		public void StopLoading()
		{
			_pendingEntry?.StopLoading();
			_pendingEntry = null;
		}
	}
}