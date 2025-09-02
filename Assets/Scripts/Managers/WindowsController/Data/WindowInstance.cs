using Common;
using UnityEngine;

namespace Managers
{
	public class WindowInstance
	{
		private BaseWindow instance;

		public BaseWindow Window
		{
			get { return instance; }
			set { instance = value; }
		}

		private WindowProperties properties;

		public WindowProperties Properties
		{
			get { return properties; }
			set { properties = value; }
		}

		public WindowInstance(WindowProperties properties)
		{
			this.Properties = properties;
		}

		public void Destroy()
		{
			GameObject.Destroy(instance.gameObject);
			instance = null;
		}
	}
}