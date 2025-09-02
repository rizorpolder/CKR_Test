using System.Collections.Generic;
using Network.RestApi;

public class NetworkManager
{
	private readonly List<ARequest> _queue = new();

	private ARequest _runningRequest;
	private bool _isQueueEmpty => _queue.Count == 0;

	public void Add(ARequest request)
	{
		_queue.Add(request);
		if (_runningRequest == null)
			Run();
	}

	public bool Remove(ARequest request)
	{
		var result = _queue.Remove(request);
		if (result) request.Abort();

		return result;
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

		if (_queue.Count > 0)
			if (_runningRequest == null)
			{
				var request = _queue[0];
				_queue.RemoveAt(0);

				request.AddReceivedHandler(OnRequestCompleted);
				request.Make();
				_runningRequest = request;
			}
	}

	private void OnRequestCompleted(ARequest obj)
	{
		_runningRequest = null;
		Run();
	}
}