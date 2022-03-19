using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api.Services;

public class LemmatizerService : ILemmatizerService
{
    private readonly IConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public LemmatizerService(IConfiguration configuration)
    {
        _factory = new ConnectionFactory() {HostName = "localhost"};
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public async Task Hello()
    {
        await Task.Run(() =>
        {
            string message = "Hello from c#!";
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish("",
                "test",
                null,
                body);
        });
    }

    public async Task<string> Get(string token)
    {
        
        var props = _channel.CreateBasicProperties();
        props.ReplyTo = "replyQueue";
        var body = Encoding.UTF8.GetBytes(token);
        _channel.BasicPublish("", "", props, body);
    }
}