using RabbitMQ.Client;
using System;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            DirectExchangeConsumer.Consume(channel);
          

            Console.ReadLine();
        }
    }
}
