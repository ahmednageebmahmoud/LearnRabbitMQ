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
                 Message Persistence Concept
                 true: In this case, the messages will be stored in memory, which means they will be durable, so the exchange will be lost if the RabbitMQ server restarts. 
                 false: In this case, the message will be stored on disk and in memory; it is in mean transit, so the exchange will be read from disk and stored in memory if the server crashes or restarted.
                 */
                durable: false 
                );
        }



    }
}
