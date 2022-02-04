using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityWatch.DeviceSimulator
{
    public partial class frmSimulator : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IMqttClient Client;

        private string Server = "";

        public frmSimulator()
        {
            InitializeComponent();
            log.Info("Simulator startup");

            txtDigitalDeviceUniqueId.Text = Guid.NewGuid().ToString().Substring(0, 8);

            var factory = new MqttFactory();
            Client = factory.CreateMqttClient();

            Client.UseDisconnectedHandler(async e =>
            {
                log.Error("MQTT client disconnected from server, retrying....");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    var options = new MqttClientOptionsBuilder().WithTcpServer(Server).Build();
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

                AddToLog($"[{e.ApplicationMessage.Topic}] : {e.ApplicationMessage.Payload}");
            });

            Client.UseConnectedHandler(e => { AddToLog("Connection: " + e.ToString()); });
        }

        private void AddToLog(string message)
        {
            log.Info(message);

            txtLog.BeginInvoke(new Action(() =>
            {
                txtLog.Text += $"{DateTime.Now} - {message}{Environment.NewLine}";
            }));
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            Server = txtServer.Text;

            AddToLog("Connecting to " + Server);

            var options = new MqttClientOptionsBuilder().WithTcpServer(Server).Build();

            await Client.ConnectAsync(options);
        }

        private void btnSendDigitalDeviceState_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                AddToLog("Client not connected");
                return;
            }

            Client.PublishAsync(new MqttApplicationMessage()
            {
                Topic = txtDeviceTopic.Text,
                Payload = ASCIIEncoding.ASCII.GetBytes($"{txtDigitalDeviceUniqueId.Text};{chkState.Checked}")
            });
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                AddToLog("Client not connected");
                return;
            }

            Random random = new Random();

            for(int i = 0; i<100; i++)
            {
                Client.PublishAsync(new MqttApplicationMessage()
                {
                    Topic = txtDeviceTopic.Text,
                    Payload = ASCIIEncoding.ASCII.GetBytes($"{Guid.NewGuid().ToString().Substring(0, 8)};{random.NextDouble()<0.5}")
                });
            }
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {

            if (!Client.IsConnected)
            {
                AddToLog("Client not connected");
                return;
            }

            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                Client.PublishAsync(new MqttApplicationMessage()
                {
                    Topic = txtDeviceTopic.Text,
                    Payload = ASCIIEncoding.ASCII.GetBytes($"{Guid.NewGuid().ToString().Substring(0, 8)};{random.NextDouble() < 0.5}")
                });
            }
        }
    }
}
