using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	[Serializable]
	[CreateAssetMenu(menuName = "Configs/Windows Config", fileName = "WindowsConfig")]
	public class WindowsConfig : ScriptableObject
	{
		public List<WindowProperties> windows;
	}
}