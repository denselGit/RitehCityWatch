using CityWatch.Common.Device;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityWatch.Server.Services
{
    public class RabbitMqService : IHostedService
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IConfiguration Configuration;

        private Thread MainThread = null;
        private bool Running = false;

        private string Server = "";
        private string QueueName = "";
        private ConcurrentQueue<PayloadMessage> InputQueue = new ConcurrentQueue<PayloadMessage>();

        public RabbitMqService(IConfiguration configuration)
        {
            Configuration = configuration;

            Server = Configuration.GetSection("RabbitMQ")["Server"];
            QueueName = Configuration.GetSection("RabbitMQ")["Queue"];
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            log.Info("Starting RabbitMQ service");

            var factory = new ConnectionFactory() { HostName = Server };

            log.Info("Connecting to RabbitMQ at " + factory.HostName);

            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        log.Info("Received new message");

                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        InputQueue.Enqueue(new PayloadMessage() { Payload = body, Topic = "" });
                    };

                    channel.BasicConsume(queue: QueueName,
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error connecting to RabbitMQ");
            }

            Running = true;
            MainThread = new Thread(Worker);
            MainThread.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.Info("Stopping RabbitMQ service");

            Running = false;
            MainThread?.Join();

            return Task.CompletedTask;
        }

        private void Worker()
        {
            while(Running)
            {
                Thread.Sleep(100);
            }
        }
    }
}
