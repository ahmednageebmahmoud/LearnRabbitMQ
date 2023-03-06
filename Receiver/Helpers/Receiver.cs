using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver.Helpers
{
    public class Receive
    {

        /// <summary>
        /// Send Message To Specific Exchange And Spcifc RoutingKey
        /// </summary>
        public void ReceiveMessage(string queueName)
        {
            var factory = new Connection().Open();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

             
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message Received {message}");
            };

            channel.BasicConsume(queueName,
                     autoAck: true,
                     consumer: consumer);



        }
    }
}
