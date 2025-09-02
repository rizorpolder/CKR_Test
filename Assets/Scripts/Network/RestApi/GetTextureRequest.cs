using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
	public class GetTextureRequest
	{
		private UnityWebRequest _textureRequest;
		private readonly string _url;

		public GetTextureRequest(string textureUrl)
		{
			_url = textureUrl;
		}

		public async UniTask<Texture2D> Make()
		{
			_textureRequest = UnityWebRequestTexture.GetTexture(_url);
			await _textureRequest.SendWebRequest();

			if (_textureRequest.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Error downloading image: " + _textureRequest.error);
			}
			else
			{
				var texture = ((DownloadHandlerTexture) _textureRequest.downloadHandler).texture;
				return texture;
			}

			return null;
		}

		public void Abort()
		{
			_textureRequest?.Abort();
		}
	}
}