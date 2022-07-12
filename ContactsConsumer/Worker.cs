using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        public IModel _channel;
        private string _queueName;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            InitialiseRabbitMQ();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                _logger.LogInformation("Message received: {time}", DateTimeOffset.Now);

                var body = ea.Body;
                var notificatonMessage = Encoding.UTF8.GetString(body.ToArray());
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            return Task.CompletedTask;

        }

        private void InitialiseRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri($"amqp://{_configuration["RabbitMQ:User"]}:{_configuration["RabbitMQ:Password"]}@{_configuration["RabbitMQ:Host"]}:{_configuration["RabbitMQ:Port"]}")
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _configuration["RabbitMQ:Trigger"], type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: _configuration["RabbitMQ:Trigger"],
                routingKey: "");

            _connection.ConnectionShutdown += ConnectionShutdown;

            _logger.LogInformation("RabbitMQ Subscriber Message Bus connected.");
        }

        private void ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation($"RabbitMQ Message Bus shutdown: {e.Cause}");
        }
    }
}
