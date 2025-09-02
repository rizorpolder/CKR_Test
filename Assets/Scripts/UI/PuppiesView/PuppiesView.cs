using Common;

namespace UI.PuppiesView
{
	public class PuppiesView : ObjectsPool<PuppiesEntry>
	{
		public void Initialize()
		{
			InitializePool();
		}

		private void Func()
		{
			var items = this.GetActiveItems();
			foreach (var puppiesEntry in items)
			{
			}
		}
	}
}