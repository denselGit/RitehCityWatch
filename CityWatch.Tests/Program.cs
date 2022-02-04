using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;

namespace CityWatch.Tests
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select benchmark to run. 1 for localhost broker, 2 for MqttNet");
            var option = Console.ReadLine();
            switch(option)
            {
                case "1":
                    BenchmarkRunner.Run<MqttBench>();
                    break;
                case "2":
                    BenchmarkRunner.Run<MqttBenchLocalhost>();
                    break;
                default:
                    Console.WriteLine("Unknown option");
                    break;
            }

         
            Console.ReadKey();
        }
    }
}
