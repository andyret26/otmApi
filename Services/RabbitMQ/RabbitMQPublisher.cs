using System.Text;
using OtmApi.Services.RabbitMQ;
using RabbitMQ.Client;

namespace Otm.Services.RabbitMQ;

public class RabbitMQPublisher(RabbitMQService rmq)
{
  private readonly IModel _channel = rmq.GetChannel();

  public void PublishMessage(string message)
  {
    _channel.QueueDeclare("testqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    var body = Encoding.UTF8.GetBytes(message);

    _channel.BasicPublish(exchange: "", routingKey: "testqueue", basicProperties: null, body: body);

  }
}