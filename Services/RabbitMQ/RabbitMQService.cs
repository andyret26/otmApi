using RabbitMQ.Client;

namespace OtmApi.Services.RabbitMQ;

public class RabbitMQService : IDisposable
{
  private readonly IConnection _connection;
  private readonly IModel _channel;

  public RabbitMQService(string hostName, string userName, string password)
  {
    var factory = new ConnectionFactory()
    {
      HostName = hostName,
      UserName = userName,
      Password = password,
      Port=5672,
      VirtualHost = userName
    };

    _connection = factory.CreateConnection();
    _channel = _connection.CreateModel();
  }

  public IModel GetChannel() => _channel;

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}