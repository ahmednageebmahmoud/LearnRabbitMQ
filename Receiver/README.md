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