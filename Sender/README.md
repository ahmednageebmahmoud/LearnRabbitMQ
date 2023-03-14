# LearnRabbitMQ

# #Crearte Channel
```
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();
```
# #Exchange Declare 
```
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
```

# #Queue Declare
```
    channel.QueueDeclare(queue: queueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );
```

# #Bind Queue On Exchage With Routing Key
```
    channel.QueueBind(queueName, exchangeName, routingKey,null);
```

# #Send Message To Specific Exchange
```
    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: string.Empty,
        basicProperties: null,
        body: Encoding.UTF8.GetBytes(message)
    );
```

# #Send Message To Specific Exchange And Spcifc RoutingKey
```
     channel.BasicPublish(
                exchange: exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message)
     );
```

# #Define The Exchage And Queue For Dead Message Whene Declare A Queue
```
   var Arguments = new Dictionary<string, string>();
        Arguments.Add("x-dead-letter-exchange", "myDeadMessagesExchange"); 
        Arguments.Add("x-dead-letter-routing-key", "myCheckOutMessageQueue");
    channel.QueueDeclare(
        ...,
        arguments: (IDictionary<string, object>)Arguments);
```

# #Reliable Publishing
```
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
          ...,
        mandatory: true //To tell me if this message has not moved to the queue, check the "BasicReturn" event.
    );
```

