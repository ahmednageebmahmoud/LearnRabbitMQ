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


            //To receive message session status if we set mandatory property = true, maby no queue binded with message echange
            channel.BasicReturn += (object? sender, BasicReturnEventArgs e) =>
            {
                Console.WriteLine("The Message Sent To Echange Successfully But {0}",e.ReplyText);
            };

            //To receive a delivery tag for a send message status exchange. 
            channel.BasicAcks += (object? sender, BasicAckEventArgs e)=>{
                Console.WriteLine("The Message Delivery Tag Is {0}", e.DeliveryTag);

            };

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: string.Empty,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message),
                mandatory: true //To tell me if this message has not moved to the queue, check the "BasicReturn" event.
                );

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
