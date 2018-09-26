using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka.Serialization;
using Google.Protobuf;

namespace KafkaProtobuff
{
    public class ProtoSerializer<T> : ISerializer<T> where T : IMessage<T>
    {
        public IEnumerable<KeyValuePair<string, object>>
            Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
            => config;

        public void Dispose() { }

        public byte[] Serialize(string topic, T data)
            => data.ToByteArray();
    }
}
