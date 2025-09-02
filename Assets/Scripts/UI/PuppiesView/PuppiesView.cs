using System;
using Common;
using Data;

namespace UI.PuppiesView
{
	public class PuppiesView : ObjectsPool<PuppiesEntryView>
	{
		public Action<string> OnEntryClicked;
		public void Initialize()
		{
			InitializePool();
		}

		public void UpdateView(PuppiesDataList data)
		{
			for (var index = 0; index < data.data.Count; index++)
			{
				var entry = data.data[index];
				var item = GetItem();
				item.Initialize(index, entry);
			}
		}

		private void Func()
		{
			var items = this.GetActiveItems();
			foreach (var puppiesEntry in items)
			{
				puppiesEntry.OnEntryClicked += OnEntryClicked;
			}
		}
	}
}