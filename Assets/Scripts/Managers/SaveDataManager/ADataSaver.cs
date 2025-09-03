namespace Managers.SaveDataManager
{
	public abstract class ADataSaver
	{
		public abstract void SaveData<T>(T data, string key);
		public abstract T LoadData<T>(string key);
	}
}