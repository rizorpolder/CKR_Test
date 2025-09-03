using System.Collections.Generic;
using Network.RestApi;

namespace Managers
{
	public class NetworkManager
	{
		private readonly List<ARequest> _queue = new();

		private ARequest _runningRequest;
		private bool _isQueueEmpty => _queue.Count == 0;

		public void Add(ARequest request)
		{
			_queue.Add(request);
			if (_runningRequest == null)
			{
				Run();
			}
		}

		public void Remove(ARequest request)
		{
			var result = _queue.Remove(request);

			if (result)
				return;

			if (_runningRequest == null || !_runningRequest.Equals(request))
				return;

			_runningRequest.Abort();
			_runningRequest = null;
			Run();
		}

		public void Terminate()
		{
			_queue.Clear();
			_runningRequest.Abort();
		}

		private void Run()
		{
			if (_isQueueEmpty)
				return;

			if (_queue.Count <= 0)
				return;

			if (_runningRequest != null)
				return;

			var request = _queue[0];
			_queue.RemoveAt(0);

			request.AddReceivedHandler(OnRequestCompleted);
			_runningRequest = request;

			request.Make();
		}

		private void OnRequestCompleted(ARequest obj)
		{
			_runningRequest = null;
			Run();
		}
	}
}