using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Network.RestApi
{
	public abstract class GetRequest<TResponseData> : ARequest
	{
		private UnityWebRequest _request;
		protected override string Method => "GET";

		public event Action<Response<TResponseData>> OnResponse = r => { };

		public override async void Make()
		{
			_request = new UnityWebRequest(Uri, Method);
			_request.downloadHandler = new DownloadHandlerBuffer();
			await _request.SendWebRequest();
			var data = ParseResponse(_request);
			NotifyComplete();
			OnResponse?.Invoke(data);
		}

		private Response<TResponseData> ParseResponse(UnityWebRequest request)
		{
			if (request.result != UnityWebRequest.Result.Success) return Response<TResponseData>.Failed();

			var data = default(TResponseData);
			try
			{
				data = ParseResponseData(request.downloadHandler.data ?? Array.Empty<byte>());
			}
			catch (Exception)
			{
				// ignore
			}

			return Response<TResponseData>.Success(data);
		}

		protected abstract TResponseData ParseResponseData(byte[] data);

		public override void Abort()
		{
			_request?.Abort();
		}
	}
}