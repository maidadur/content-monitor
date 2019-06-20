namespace Maid.Core
{
	public interface IMessageConsumer
	{
		void Process(byte[] data);
	}
}
