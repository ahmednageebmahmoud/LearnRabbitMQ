# LearnRabbitMQ

# #Recive Message
```
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
```

# #Remove a message from the current queue and move it to the dead message queue
```
   catch (Exception)
   {
       channel.BasicReject(
       deliveryTag: ea.DeliveryTag, //Message Refrance
       /*
        true: Requeue Aging in the Current Queue 
        false: Remove a message from the current queue and move it to the dead message queue. 
        */
       requeue: false
       );
   }
```