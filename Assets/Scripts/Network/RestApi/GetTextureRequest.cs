using Cysharp.Threading.Tasks;
using Network.RestApi;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
	public class GetTextureRequest<TData> : ARequest
	{
		protected override string Method => "GET";

		private string _url;
		protected override string Uri => _url;

		private UnityWebRequest _textureRequest;

		public GetTextureRequest(string textureUrl)
		{
			_url = textureUrl;
		}

		public override async void Make()
		{
			_textureRequest = UnityWebRequestTexture.GetTexture(_url);
			await _textureRequest.SendWebRequest();

			if (_textureRequest.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Error downloading image: " + _textureRequest.error);
			}
			else
			{
				Texture2D texture = ((DownloadHandlerTexture)_textureRequest.downloadHandler).texture;
				// if (targetImage != null)
				// {
				// 	targetImage.texture = texture;
				// }
				// else
				// {
				// 	Debug.LogWarning("Target RawImage not assigned.");
				// }
			}
		}

		public override void Abort()
		{
			_textureRequest?.Abort();
		}
	}
}