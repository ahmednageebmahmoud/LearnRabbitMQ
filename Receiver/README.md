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
   try{
       ...
   }
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

# #Consumer Acknowledgement
## #Apply Auto Positive Acknowledgement To Remove Message From Queue Directly After Recive
```
      channel.BasicConsume(
            ...
            autoAck: true,
            ...
      );
```
## #Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That.
```
      //Set autoAck= false to allow use manual
      channel.BasicConsume(
            ...
            autoAck: false,
            ...
      );
        
      //Write the fellowing code in reciver event
      try
      {
          ...
      }
      catch (Exception)
      {
          //Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That
          channel.BasicAck(
              deliveryTag: ea.DeliveryTag, //Message Refrance
              multiple:false  
              );
      }

      //Optional Add Subscribtion On Nagative Event Acknowledgement
      channel.BasicNacks += (object? sender, BasicNackEventArgs e) =>
        {
            Console.WriteLine("The Message Is Nagtive Acknowledgement {01} ",e.DeliveryTag);
        };
```

## #Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That And we Cane Requeue This Message
```
      //Set autoAck= false to allow use manual
      channel.BasicConsume(
            ...
            autoAck: false,
            ...
      );
        
      //Write the fellowing code in reciver event
      try
      {
          ...
      }
      catch (Exception)
      {
            channel.BasicNack(
                deliveryTag: ea.DeliveryTag, //Message Refrance
                multiple: false,
                requeue: false //true: Requeue Aging in the Current Queue, false: Remove a message from the current queue and move it to the dead message queue. 
            );
      }
```
## #Acknowledgement Events
```
    channel.BasicNacks += (object? sender, BasicNackEventArgs e) =>
    {
        Console.WriteLine("The Message Is Nagtive Acknowledgement {01} ",e.DeliveryTag);
    };
    channel.BasicAcks  += (object? sender, BasicAckEventArgs e) =>
    {
        Console.WriteLine("The Message Is Acknowledgement {01} ", e.DeliveryTag);
    }; 
```

# #Consumer Prefetch, Aply Consumer Prefetch On Channel
```
    channel.BasicQos(
        prefetchSize: 0,
        prefetchCount: 2,//Per consumer limit
        global: true // true:shared across all consumers on the connection , false:shared across all consumers on the channel
    );
```

# #Consumer Priorities
```
  channel.BasicConsume(
      ...
      arguments: new Dictionary<string, object> { { "x-priority", 5 } }
  );
```