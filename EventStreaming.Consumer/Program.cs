using System;
using System.Text.Json;
using System.Threading;
using Confluent.Kafka;
using EventStreaming.Domain.Models;

namespace EventStreaming.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must input a consumer group id");
                return;
            }

            string groupId = args[0];
            Console.WriteLine($"Consumer in group id: {groupId}");
            
            Console.WriteLine("Running consumer");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = groupId
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