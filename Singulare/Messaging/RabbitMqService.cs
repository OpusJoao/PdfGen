using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Singulare.Messaging
{
    public class RabbitMqService : IMessageBusService
    {
        private IConnection _connection;

        private IModel _channel;

        private const string _exchange = "process-report";

        public RabbitMqService()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "host.docker.internal"
            };

            _connection = connectionFactory.CreateConnection("quote-report-publisher");
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "process-report", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
        public void publish(object data, string routingKey)
        {
            var payload = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(string.Empty, routingKey, null, byteArray);
        }
    }
}
