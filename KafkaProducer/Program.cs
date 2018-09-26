using System;
using System.Threading.Tasks;

namespace KafkaProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var producerService = new ProducerService();
            producerService.ProduceMessageAsync();
        }
    }
}
