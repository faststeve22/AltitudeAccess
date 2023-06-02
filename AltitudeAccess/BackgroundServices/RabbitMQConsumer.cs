using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using AltitudeAccess.PresentationLayer.DTOs;

namespace AltitudeAccess.BackgroundServices
{
    public class RabbitMQConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private ILogger _logger;
        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger)
        {
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 0000,
                UserName = "EnterUsername",
                Password = "EnterPassword"
            };
            this._connection = factory.CreateConnection();
            this._channel = _connection.CreateModel();
            {
                _channel.ExchangeDeclare(exchange: "user_events", type: "direct");
                var queueName = _channel.QueueDeclare(queue: "ErrorQueue").QueueName;
                _channel.QueueBind(queue: queueName, exchange: "user_events", routingKey: "Error");
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var exception = JsonConvert.DeserializeObject<ExceptionDTO>(message);
                    _logger.LogError($"Received exception... Source:{exception.Source}, Message:{exception.Message}, InnerException Source: {exception.InnerException.Source}, InnerException Message{exception.InnerException.Message}");
                    throw new Exception("Could not complete user creation");
                };
                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                return Task.CompletedTask;
            }
        }
    }
}
