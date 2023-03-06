using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Receiver.Helpers
{
    public class Queue
    {
        /// <summary>
        /// Create Queue
        /// </summary>
        public void Declare(string queueName)
        {
            var factory = new Connection().Open();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

       
        }

        /// <summary>
        /// Bind Queue On Exchage With Routing Key
        /// </summary>
        public void Bind(string queueName, string exchangeName, string routingKey)
        {
            var factory = new Connection().Open();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueBind(queueName, exchangeName, routingKey,null);
        }


    }
}
