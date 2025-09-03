using System;
using AudioManager.Runtime.Core.Manager;
using Managers;
using UnityEngine;
using Zenject;

namespace Common
{
	public class BaseWindow : MonoBehaviour
	{
		[Inject] private WindowsController _controller;
		[Inject] protected ManagerAudio _managerAudio;
		[SerializeField] private string ID;

		private WindowStatus _status = WindowStatus.Hidden;
		public WindowStatus Status => _status;

		//Animation behaviour, sounds, etc
		public void Show(Action callback = null)
		{
			_managerAudio.PlayAudioClip(TAudio.swish_in.ToString());
			gameObject.SetActive(true);
			_status = WindowStatus.Shown;
		}

		public void Hide(Action callback = null)
		{
			_managerAudio.PlayAudioClip(TAudio.swish_out.ToString());
			gameObject.SetActive(false);
			_status = WindowStatus.Hidden;
		}

		public virtual void Close()
		{
			_controller.Hide(ID);
		}
	}

	public enum WindowStatus
	{
		Hidden,
		Shown,
	}
}