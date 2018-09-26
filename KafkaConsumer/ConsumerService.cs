using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Kafka;
using KafkaProtobuff;

namespace KafkaConsumer
{
    public class ConsumerService
    {
        private readonly Dictionary<string, object> _kafkaConfiguration;

        public ConsumerService()
        {
            _kafkaConfiguration = new Dictionary<string, object>()
            {
                { "group.id", "test-consumer-group" },
                { "bootstrap.servers", "localhost:7092" },
                { "auto.commit.interval.ms", 5000 },
                { "auto.offset.reset", "earliest" },
                { "socket.blocking.max.ms", 1 }
            };
        }

        public void ConsumeMessage()
        {
            using (var consumer = new Consumer<Null, OrderMessage>(_kafkaConfiguration, null, new ProtoDeserializer<OrderMessage>()))
            {
                consumer.OnMessage += (_, msg)
                    => Console.WriteLine($"Order with '{msg.Value.Id}' has created from: {msg.TopicPartitionOffset}");

                consumer.OnError += (_, error)
                    => Console.WriteLine($"Error: {error}");

                consumer.OnConsumeError += (_, msg)
                    => Console.WriteLine($"Consume error ({msg.TopicPartitionOffset}): {msg.Error}");

                consumer.Subscribe("my-topic57");

                while (true)
                {
                    consumer.Poll(TimeSpan.FromMilliseconds(100));
                }
            }
        }
    }
}
