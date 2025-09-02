using System;
using UnityEngine;

namespace Managers
{
	[Serializable]
	public class WindowProperties
	{
		public string name;
		public WindowType type;
		public GameObject assetReference; // addressable
		public bool IsCached = false;
	}
}