using CityWatch.Common;
using CityWatch.Common.Device;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityWatch.Server.Services
{
    public class MqttService : IHostedService
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IConfiguration Configuration;
        private IMqttClient Client;

        private Thread MainThread = null;
        private bool Running = false;

        private string Server = "";
        private List<string> TopicsList = new List<string>();

        private ConcurrentQueue<PayloadMessage> InputQueue = new ConcurrentQueue<PayloadMessage>();

        public MqttService(IConfiguration configuration)
        {
            Configuration = configuration;

            Server = Configuration.GetSection("MQTT")["Server"];
            TopicsList = Configuration.GetSection("MQTT")["Topics"].Split(',').ToList();

            var factory = new MqttFactory();
            Client = factory.CreateMqttClient();

            Client.UseDisconnectedHandler(async e =>
            {
                log.Error("MQTT client disconnected from server, retrying....");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    var options = new MqttClientOptionsBuilder().WithWebSocketServer(Server).Build();
                    await Client.ConnectAsync(options, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    log.Error(ex, "Error connecting to MQTT broker!");
                }
            });

            Client.UseApplicationMessageReceivedHandler(e =>
            {
                log.Debug($"Receiving: {e.ApplicationMessage.Topic} \\ {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");

                InputQueue.Enqueue(new PayloadMessage() { Topic = e.ApplicationMessage.Topic, Payload = e.ApplicationMessage.Payload });
            });

            Client.UseConnectedHandler(async e =>
            {
                log.Info("Connected to broker, subscribing...");

                foreach(var topic in TopicsList)
                {
                    log.Debug("Subscribed to " + topic);
                    await Client.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter(topic.Trim()).Build());
                }
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            log.Info("Starting MQTT service...");

            Running = true;
            MainThread = new Thread(Worker);
            MainThread.Start();

            try
            {
                var options = new MqttClientOptionsBuilder().WithTcpServer(Server).Build();

                await Client.ConnectAsync(options, cancellationToken);
            }
            catch(Exception ex)
            {
                log.Error(ex, "Error connecting to MQTT broker");

                await Task.CompletedTask;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.Info("Stopping MQTT service...");
            Running = false;

            MainThread?.Join();

            return Task.CompletedTask;
        }

        private void Worker()
        {
            while(Running)
            {
                if (InputQueue.TryPeek(out PayloadMessage message))
                {
                    if (ProcessMessage(message))
                    {
                        InputQueue.TryDequeue(out _);
                    }
                }

                Thread.Sleep(100);
            }
        }

        private bool ProcessMessage(PayloadMessage message)
        {
            bool result = false;

            switch(message.Topic)
            {
                case "topic1":
                    result = ProcessTopic1(message);
                    break;
                case "/topic2":
                    result = ProcessTopic2(message);
                    break;
                case "/device":
                    result = ProcessDeviceMessage(message);
                    break;
                default:
                    result = ProcessUnknown(message);
                    break;
            }

            return result;
        }

        private bool ProcessTopic1(PayloadMessage message)
        {
            log.Debug("Processing topic1 message...");
            return true;
        }

        private bool ProcessTopic2(PayloadMessage message)
        {
            log.Debug("Processing topic2 message...");
            return true;
        }

        private bool ProcessUnknown(PayloadMessage message)
        {
            log.Debug("Processing unknown message...");
            return true;
        }

        private bool ProcessDeviceMessage(PayloadMessage message)
        {
            log.Debug("Processing device message...");

            var msg = ASCIIEncoding.ASCII.GetString(message.Payload);
            var payloadData = msg.Split(';');

            if (payloadData.Length != 2)
            {
                log.Error("Invalid message received!");
                return false;
            }

            var device = payloadData[0];
            var payload = payloadData[1];

            ServiceManager.MessageQueue.Enqueue(new Message(Message.EMessageType.Device, device, payload));

            return true;
        }
    }
}
