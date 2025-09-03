namespace Managers.Clicker
{
	public interface IClickerData
	{
		public int UserCurrency { get; }
		public int UserEnergy { get; }
		public int CurrencyReward { get; }
	}
}