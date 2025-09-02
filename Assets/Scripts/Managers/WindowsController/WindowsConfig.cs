using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	[Serializable]
	[CreateAssetMenu(menuName = "Project/Windows Config")]
	public class WindowsConfig : ScriptableObject
	{
		public List<WindowProperties> windows;
	}
}