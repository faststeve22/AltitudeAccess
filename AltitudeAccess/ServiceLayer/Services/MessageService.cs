using AltitudeAccess.ServiceLayer.Models;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AltitudeAccess.ServiceLayer.Services
{
    public class MessageService : IMessageService
    {
        public void PublishUserInfo(User createdUser)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 0000,
                UserName = "EnterUsername",
                Password = "EnterPassword"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "user_events", type: "direct");

                var user = new { UserId = createdUser.UserId, Username = createdUser.Username, FirstName = createdUser.FirstName, LastName = createdUser.LastName, EmailAddress = createdUser.EmailAddress };
                var message = JsonConvert.SerializeObject(user);
                var body = Encoding.UTF8.GetBytes(message);
                 
                channel.BasicPublish(exchange: "user_events", routingKey: "Info", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

        }
    }
}
