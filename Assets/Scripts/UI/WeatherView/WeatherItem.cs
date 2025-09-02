using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WeatherView
{
	public class WeatherItem : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _description;

		public WeatherItem SetIcon(Sprite icon)
		{
			_icon.sprite = icon;
			return this;
		}

		public WeatherItem
			SetDescription(string day, string temperature, string unit) // можно локализационные ключи прокидывать
		{
			_description.text = $"{day} - {temperature} {unit}";
			return this;
		}
	}
}