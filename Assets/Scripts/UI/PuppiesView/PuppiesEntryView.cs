using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PuppiesView
{
	public class PuppiesEntryView : MonoBehaviour
	{
		public event Action<string> OnEntryClicked = _ => { };

		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _index;
		[SerializeField] private TextMeshProUGUI _description;
		[SerializeField] private GameObject _loadingImage;

		private string _itemID;

		private void Start()
		{
			_button.onClick.AddListener(OnClickButtonHandler);
		}

		public void Initialize(int index, PuppiesEntry data)
		{
			_itemID = data.id;
			_index.text = index.ToString();
			_description.text = data.attributes.name;
		}

		private void OnClickButtonHandler()
		{
			OnEntryClicked.Invoke(_itemID);
			_loadingImage.SetActive(true);
		}
	}
}