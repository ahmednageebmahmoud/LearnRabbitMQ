using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender.Helpers
{
    public class Exchange
    {
        /// <summary>
        /// Create Exchange
        /// </summary>
        public void Declare(string  exchangeName)
        {
            var factory =  new Connection().Open();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: exchangeName,
                /*
                 direct: To Link (Bind) Queue With Exchange By Routing Key
                 fanout:  To Link (Bind) Queue With Exchange Without Routing Key
                 headers: Exchange Depand Header Data To Meke The Routing 
                 topic: To Link (Bind) Queue With Exchange By Routing Key But Here We Can Use Pattren To Define The Key
                 */
                type: "fanout",
                /*
                 true: Delete exchange if the linked queue unbind
                 false: Not Delete exchange if the linked queue unbind
                 */
                autoDelete: true,
                /*
                 true: it's mean durable so the exchange not removed if the RabbitMQ server restarted 
                 false: it's mean transit so the exchange will removed if the RabbitMQ server restarted and recreate it if need to use it agine
                 */
                durable: false 
                );
        }



    }
}
