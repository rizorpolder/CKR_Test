using AudioManager.Runtime.Core.Manager;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class InfoPopup : BaseWindow
	{
		[SerializeField] private TextMeshProUGUI _headerText;
		[SerializeField] private TextMeshProUGUI _descriptionText;
		[SerializeField] private Button _okButton;

		private void Start()
		{
			_okButton.onClick.AddListener(OkButtonClickHandler);
		}

		public void Initialize(string header, string description)
		{
			_headerText.text = header;
			_descriptionText.text = description;
		}

		private void OkButtonClickHandler()
		{
			_managerAudio.PlayAudioClip(TAudio.click.ToString());
			this.Close();
		}
	}
}