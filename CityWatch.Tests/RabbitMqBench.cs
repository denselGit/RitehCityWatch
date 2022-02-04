using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityWatch.Tests
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [RPlotExporter, RankColumn]
    [MemoryDiagnoser]
    internal class RabbitMqBench
    {
        [GlobalSetup]
        public void Setup()
        {

        }

        [GlobalCleanup]
        public void Teardown()
        {

        }

        [Benchmark]
        public void Send_10000_Messages()
        {
            for (var i = 0; i < 10000; i++)
            {
                //_mqttClient.PublishAsync(_message).GetAwaiter().GetResult();
            }
        }
    }

    class Receive
    {
        public void Test()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                };

                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);


            }
        }
    }
}
