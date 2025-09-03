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

		[SerializeField] private string ID;

		//Animation behaviour, sounds, etc
		public void Show(Action callback = null)
		{
			ManagerAudio.SharedInstance.PlayAudioClip(TAudio.swish_in.ToString());

			gameObject.SetActive(true);
		}

		public void Hide(Action callback = null)
		{
			ManagerAudio.SharedInstance.PlayAudioClip(TAudio.swish_out.ToString());

			gameObject.SetActive(false);
		}

		public virtual void Close()
		{
			_controller.Hide(ID);
		}
	}
}