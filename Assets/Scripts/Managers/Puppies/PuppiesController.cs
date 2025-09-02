using UI.PuppiesView;
using UnityEngine;
using Zenject;

namespace Managers.Puppies
{
	public class PuppiesController : MonoBehaviour
	{
		[Inject] NetworkManager _networkManager;

		[SerializeField] private PuppiesView view;

		private void Start()
		{

		}


	}
}