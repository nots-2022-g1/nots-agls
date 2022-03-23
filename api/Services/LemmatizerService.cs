using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api.Services;

public class LemmatizerService : ILemmatizerService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _replyQueueName;
    private readonly AsyncEventingBasicConsumer _consumer;
    private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
    private readonly IBasicProperties _props;

    public LemmatizerService(IConfiguration configuration)
    {
        var factory = new ConnectionFactory() {HostName = "localhost"};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _replyQueueName = _channel.QueueDeclare().QueueName;
        _consumer = new AsyncEventingBasicConsumer(_channel);
        
        _props = _channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        _props.CorrelationId = correlationId;
        _props.ReplyTo = _replyQueueName;

        _consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                respQueue.Add(response);
            }
        };
        _channel.BasicConsume(_consumer, _replyQueueName, true);
    }
    
    private event AsyncEventHandler<BasicDeliverEventArgs> OnReceived()
    {
        
    } 


    public async Task<string> Get(string token)
    {
        
        var props = _channel.CreateBasicProperties();
        props.ReplyTo = "replyQueue";
        var body = Encoding.UTF8.GetBytes(token);
        _channel.BasicPublish("", "", props, body);
    }
}