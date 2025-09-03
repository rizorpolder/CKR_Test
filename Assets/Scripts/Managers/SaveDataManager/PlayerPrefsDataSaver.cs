using UnityEngine;

namespace Managers.SaveDataManager
{
	public class PlayerPrefsDataSaver : ADataSaver
	{
		public override void SaveData<T>(T data, string key)
		{
			var str = Newtonsoft.Json.JsonConvert.SerializeObject(data);
			PlayerPrefs.SetString(key, str);
		}

		public override T LoadData<T>(string key)
		{
			var data = PlayerPrefs.GetString(key, string.Empty);

			if (!string.IsNullOrEmpty(data))
			{
				return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
			}

			return default(T);
		}
	}
}