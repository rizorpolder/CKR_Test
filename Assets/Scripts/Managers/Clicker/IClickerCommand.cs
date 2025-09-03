namespace Managers.Clicker
{
	public interface IClickerCommand
	{
		public void AddEnergy(int energyValue);
		public void AddCurrency(int currencyValue);
	}
}