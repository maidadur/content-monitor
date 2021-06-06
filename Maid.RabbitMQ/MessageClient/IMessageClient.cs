namespace Maid.RabbitMQ
{
	public interface IMessageClient
	{
		void SendMessage(string queueName, object data);
	}
}