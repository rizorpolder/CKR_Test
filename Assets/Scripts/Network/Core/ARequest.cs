using System;

namespace Network.RestApi
{
	public abstract class ARequest
	{
		protected event Action<ARequest> onResponseReceived;
		protected abstract string Method { get; }
		protected abstract string Uri { get; }
		public abstract void Make();

		public abstract void Abort();

		public void AddReceivedHandler(Action<ARequest> handler)
		{
			onResponseReceived += handler;
		}

		protected void NotifyComplete()
		{
			var runningEvent = onResponseReceived;
			onResponseReceived = null;
			runningEvent?.Invoke(this);
		}
	}
}