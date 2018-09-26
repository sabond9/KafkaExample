using System;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerService = new ConsumerService();
            consumerService.ConsumeMessage();
        }
    }
}
