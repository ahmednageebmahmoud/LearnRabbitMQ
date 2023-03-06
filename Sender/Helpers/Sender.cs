using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender.Helpers
{
    public class Send
    {
        /// <summary>
        /// Send Message To Specific Exchange
        /// </summary>
        public void ExchangeSendMessage(string message, string exchangeName)
        {
            var factory = new Connection().Open();
            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();


            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: string.Empty,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message));
            Console.WriteLine($"Message Sent {message}");



        }

        /// <summary>
        /// Send Message To Specific Exchange And Spcifc RoutingKey
        /// </summary>
        public void ExchangeSendMessage(string message, string exchangeName, string routingKey)
        {
            var factory = new Connection().Open();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel(); 
            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message));
            Console.WriteLine($"Message Sent {message}");
        }

    }
}
