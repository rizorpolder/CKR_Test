using TMPro;
using UnityEngine;

namespace UI.Clicker.UI.HUD
{
	public abstract class AResourcePanel : MonoBehaviour
	{
		[SerializeField] protected TextMeshProUGUI _resourceValueText;

		public virtual void SetResourceText(int value)
		{
			_resourceValueText.text = value.ToString();
		}
	}
}