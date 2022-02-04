using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Server;
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
    public class MqttBenchLocalhost
    {
        IMqttServer _mqttServer;
        IMqttClient _mqttClient;
        MqttApplicationMessage _message;

        [GlobalSetup]
        public void Setup()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
            _mqttServer = factory.CreateMqttServer();

            var serverOptions = new MqttServerOptionsBuilder().WithDefaultEndpointPort(2883).Build();
            _mqttServer.StartAsync(serverOptions).GetAwaiter().GetResult();

            var clientOptions = new MqttClientOptionsBuilder()
              .WithTcpServer("127.0.0.1", 2883).Build();

            _mqttClient.ConnectAsync(clientOptions).GetAwaiter().GetResult();

            _message = new MqttApplicationMessageBuilder()
                .WithTopic("A")
                .Build();
        }

        [Benchmark]
        public void Send_10000_Messages()
        {
            for (var i = 0; i < 10000; i++)
            {
                _mqttClient.PublishAsync(_message).GetAwaiter().GetResult();
            }
        }
    }
}
