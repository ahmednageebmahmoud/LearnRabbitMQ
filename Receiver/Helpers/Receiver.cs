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

            //Aply Consumer Prefetch On Channel
            channel.BasicQos(
                  prefetchSize: 0,
                  prefetchCount: 2,//Per consumer limit
                  global: true // true:shared across all consumers on the connection , false:shared across all consumers on the channel
                );

            channel.BasicNacks += (object? sender, BasicNackEventArgs e) =>
            {
                Console.WriteLine("The Message Is Nagtive Acknowledgement {01} ",e.DeliveryTag);
            };
            channel.BasicAcks  += (object? sender, BasicAckEventArgs e) =>
            {
                Console.WriteLine("The Message Is Acknowledgement {01} ", e.DeliveryTag);
            };

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message Received {message}");
                }
                catch (Exception)
                {
                    //Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That
                    channel.BasicAck(
                        deliveryTag: ea.DeliveryTag, //Message Refrance
                        multiple:false  
                        );

                    //Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That And we Cane Requeue This Message
                    channel.BasicNack(
                        deliveryTag: ea.DeliveryTag, //Message Refrance
                        multiple: false,
                        requeue: false //true: Requeue Aging in the Current Queue, false: Remove a message from the current queue and move it to the dead message queue. 
                        );

                    //Remove a message from the current queue and move it to the dead message queue
                    channel.BasicReject(
                    deliveryTag: ea.DeliveryTag, //Message Refrance
                    requeue: false //true: Requeue Aging in the Current Queue, false: Remove a message from the current queue and move it to the dead message queue. 

                    );
                }
                
              
            };


            channel.BasicConsume(queueName,
                     autoAck: true,//If We Need Active Auto Positive Acknowledgement To Remove Message From Queue Directly After Recive
                     consumer: consumer);



        }

    
    }
}
