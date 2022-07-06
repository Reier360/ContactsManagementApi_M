using ContactsService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ContactsService.Services
{
    public class RabbitMQClient : IMessageBusClient
    {
        private readonly ILogger<RabbitMQClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMQClient(ILogger<RabbitMQClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri($"amqp://{_configuration["RabbitMQ:User"]}:{_configuration["RabbitMQ:Password"]}@{_configuration["RabbitMQ:Host"]}:{_configuration["RabbitMQ:Port"]}")
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: _configuration["RabbitMQ:Trigger"], type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += ConnectionShutdown;

                _logger.LogInformation("RabbitMQ Message Bus connected.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not connect to the RabbitMQ Message Bus: {e.Message}");
            }
        }

        private void ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation($"RabbitMQ Message Bus shutdown: {e.Cause}");
        }

        public void PublishMessage<T>(T Message)
        {
            var message = JsonConvert.SerializeObject(Message);

            if (_connection.IsOpen)
            {
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(
                    exchange: _configuration["RabbitMQ:Trigger"],
                    string.Empty,
                    basicProperties: null,
                    body: body);
            }
            else
            {
                _logger.LogError($"Could not publish RabbitMQ Message connection was closed: {Message}");
            }
        }
    }
}
