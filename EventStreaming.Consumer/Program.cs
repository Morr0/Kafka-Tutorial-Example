using System;
using System.Text.Json;
using Confluent.Kafka;
using EventStreaming.Domain.Models;

namespace EventStreaming.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running consumer");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "1"
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            consumer.Subscribe("Student");

            while (true)
            {
                var result = consumer.Consume(5);
                if (string.IsNullOrEmpty(result?.Message?.Value)) continue;
                
                var obj = JsonSerializer.Deserialize<StudentRecordUpdate>(result.Message.Value);

                Console.WriteLine("Received a message");
                Console.WriteLine($"Student Id: {obj.StudentId} updated state to {obj.State.ToString()}");
            }
        }
    }
}