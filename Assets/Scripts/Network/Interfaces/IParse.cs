namespace Network
{
	public interface IParse<T>
	{
		public T ParseFrom(byte[] data);
	}
}