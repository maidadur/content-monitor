using Maid.Core;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maid.RabbitMQ
{
	public class MessageQueuesManager
	{
		private static Lazy<MessageQueuesManager> _instance =
			new Lazy<MessageQueuesManager>(() => new MessageQueuesManager());

		private IModel _channel;
		private IConnection _connection;
		private IServiceProvider _serviceProvider;

		private MessageQueuesManager() {
		}

		public static MessageQueuesManager Instance => _instance.Value;

		public MessageQueuesManager ConnectToQueue(string queueName, bool overrideMessage = true) {
			if (_channel == null) {
				return Instance;
			}
			var args = overrideMessage ? new Dictionary<string, object>
				{
					{ "x-max-length", 1 },           // Only 1 message in queue
					{ "x-overflow", "drop-head" }    // Drop old message if new one arrives
				} : null;
			_channel.QueueDeclare(queue: queueName,
					 durable: false,
					 exclusive: false,
					 autoDelete: false,
					 arguments: args);
			Console.WriteLine("MessageQueuesManager. QueueDeclare " + queueName);
			return Instance;
		}

		public MessageQueuesManager Init(IServiceProvider serviceProvider, string queueUrl = null, int? port = null) {
			_serviceProvider = serviceProvider;
			Console.WriteLine($"MessageQueuesManager. Init. {queueUrl}:{port}");
			var factory = new ConnectionFactory() { HostName = queueUrl ?? "localhost", Port = port ?? 5672 };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			return Instance;
		}

		public void Publish(string queueName, byte[] data) {
			if (_channel == null) {
				return;
			}
			_channel.BasicPublish(exchange: "",
								 routingKey: queueName,
								 basicProperties: null,
								 body: data);
		}

		public MessageQueuesManager Subscribe<T>(string queueName)
				where T : IMessageConsumer {
			if (_channel == null) {
				return Instance;
			}
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, eventArgs) => {
				var body = eventArgs.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine("MessageQueuesManager. Received message: " + message);
				using (var scope = _serviceProvider.CreateScope()) {
					var subscriber = scope.ServiceProvider.GetRequiredService<T>();
					await subscriber.ProcessAsync(body);
				}
			};
			_channel.BasicConsume(queue: queueName,
								 autoAck: true,
								 consumer: consumer);
			Console.WriteLine("MessageQueuesManager. Subscribed for queue " + queueName);
			return Instance;
		}
	}
}