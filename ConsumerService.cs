namespace InfoYatirim.Consumer
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Channels;

    public class ConsumerService
    {
        private IConnection _connection;
        private IChannel _channel;
        private readonly CacheService _cacheService;

        public ConsumerService(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Create()
        {
           
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.QueueDeclareAsync(queue: "data-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonSerializer.Deserialize<Data>(message);
                if (data != null)
                {
                    _cacheService.AddData(data);
                }
            };

            await _channel.BasicConsumeAsync(queue: "data-queue", autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
