using Managers;
using Network.ApiData.Dogs;
using Network.RestApi;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Contexts
{
	public class Test : MonoBehaviour
	{
		[SerializeField] private Button _button;

		[Inject] WindowsController _windowsController;

		private void Start()
		{
			_button.onClick.AddListener(OnButtonClickHandler);
		}

		private void OnButtonClickHandler()
		{

		}
	}
}