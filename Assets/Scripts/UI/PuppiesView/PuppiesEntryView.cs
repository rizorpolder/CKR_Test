using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PuppiesView
{
	public class PuppiesEntryView : MonoBehaviour
	{
		public event Action<PuppiesEntryView> OnEntryClicked = (c) => { };

		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _indexText;
		[SerializeField] private TextMeshProUGUI _description;
		[SerializeField] private GameObject _loadingImage;

		private string _itemID;
		public string ItemID => _itemID;

		private void Start()
		{
			_button.onClick.AddListener(OnClickButtonHandler);
		}

		public void Initialize(int index, PuppiesEntry data)
		{
			_itemID = data.id;
			_indexText.text = index.ToString();
			_description.text = data.attributes.name;
		}

		private void OnClickButtonHandler()
		{
			OnEntryClicked.Invoke(this);
			_loadingImage.SetActive(true);
		}

		public void StopLoading()
		{
			_loadingImage.SetActive(false);
		}
	}
}