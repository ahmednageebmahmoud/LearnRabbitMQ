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

###Consumer Acknowledgement
# #Apply Auto Positive Acknowledgement To Remove Message From Queue Directly After Recive
```
      channel.BasicConsume(
            ...
            autoAck: true,
            ...
      );
```
# #Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That.
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
```

# #Apply Manual Positive Acknowledgement To Remove Message From Queue If We Need That And we Cane Requeue This Message
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