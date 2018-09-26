using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Kafka;
using Kafka.Data;
using KafkaProtobuff;

namespace KafkaProducer
{
    public class ProducerService
    {
        private readonly Dictionary<string, object> _kafkaConfiguration;

        public ProducerService()
        {
            _kafkaConfiguration = new Dictionary<string, object>()
            {
                { "bootstrap.servers", "localhost:7092" },
                { "socket.blocking.max.ms", 1 },
                { "acks", "all" },
                { "batch.num.messages", 1 },
                { "linger.ms", 0 },
            };
        }

        public void ProduceMessageAsync()
        {
            var random = new Random();
            var order = new OrderMessage {Id = random.Next(10000), Name = "Name"};
            using (var producer = new Producer<Null, OrderMessage>(_kafkaConfiguration, null, new ProtoSerializer<OrderMessage>()))
            {
                var message = producer.ProduceAsync("my-topic57", null, order).Result;
                producer.Flush(1);
                Console.WriteLine($"Order '{order.Id} was created'");
            }
        }
    }
}
