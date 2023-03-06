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
            true: it's mean durable so the exchange not removed if the RabbitMQ server restarted 
            false: it's mean transit so the exchange will removed if the RabbitMQ server restarted and recreate it if need to use it agine
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