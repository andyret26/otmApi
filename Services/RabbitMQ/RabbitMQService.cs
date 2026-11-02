// using RabbitMQ.Client;

// namespace OtmApi.Services.RabbitMQ;

// public class RabbitMQService : IDisposable
// {
//   private readonly IConnection _connection;
//   private readonly IChannel _channel;

//   public async Task<RabbitMQService> InitializeAsync(string hostName, string userName, string password)
//   {
//     var factory = new ConnectionFactory { HostName = hostName};
//     var connection = await factory.CreateConnectionAsync();
//     var channel = await connection.CreateChannelAsync();

//     return channel
//   }

//   public IChannel GetChannel() => _channel;

//     public void Dispose()
//     {
//         _channel?.Close();
//         _connection?.CloseAsync();
//     }
// }