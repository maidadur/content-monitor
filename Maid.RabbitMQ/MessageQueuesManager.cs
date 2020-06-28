﻿using RabbitMQ.Client;
using System;
using System.Text;
using Maid.Core;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Maid.RabbitMQ
{
	public class MessageQueuesManager
	{
		private static Lazy<MessageQueuesManager> _instance = 
			new Lazy<MessageQueuesManager>(() => new MessageQueuesManager());
		private IServiceProvider _serviceProvider;
		private IModel _channel;
		private IConnection _connection;

		private MessageQueuesManager() { }


		public static MessageQueuesManager Instance => _instance.Value;

		public MessageQueuesManager Init(IServiceProvider serviceProvider, string queueUrl = null) {
			_serviceProvider = serviceProvider;
			var factory = new ConnectionFactory() { HostName = queueUrl ?? "localhost", Port = 5672 };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			return Instance;
		}

		public MessageQueuesManager ConnectToQueue(string queueName) {
			if (_channel == null) {
				return Instance;
			}
			_channel.QueueDeclare(queue: queueName,
					 durable: false,
					 exclusive: false,
					 autoDelete: false,
					 arguments: null);
			return Instance;
		}

		public void Publish(string queueName, object data) {
			if (_channel == null) {
				return;
			}
			var bytes = data.ToBytesArray();
			_channel.BasicPublish(exchange: "",
								 routingKey: queueName,
								 basicProperties: null,
								 body: bytes);
		}

		public MessageQueuesManager Subsribe<T>(string queueName) 
				where T: IMessageConsumer  {
			if (_channel == null) {
				return Instance;
			}
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, ea) => {
				var body = ea.Body;
				var message = Encoding.UTF8.GetString(body);
				using (var scope = _serviceProvider.CreateScope()) {
					var subscriber = scope.ServiceProvider.GetRequiredService<T>();
					await subscriber.ProcessAsync(body);
				}
			};
			_channel.BasicConsume(queue: queueName,
								 autoAck: true,
								 consumer: consumer);
			return Instance;
		}
	}
}
