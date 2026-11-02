// using System.Text;
// using Microsoft.EntityFrameworkCore.Metadata;
// using OtmApi.Services.RabbitMQ;
// using RabbitMQ.Client;

// namespace Otm.Services.RabbitMQ;

// public class RabbitMQPublisher(RabbitMQService rmq)
// {
//   private readonly IChannel _channel = rmq.GetChannel();

//   public void PublishMessage(string message)
//   {
//     _channel.QueueDeclareAsync("testqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
//     var body = Encoding.UTF8.GetBytes(message);

//     _channel.BasicPublishAsync(exchange: "", routingKey: "testqueue", body: body);

//   }
// }