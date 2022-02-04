using CityWatch.Common;
using CityWatch.Common.Device;
using CityWatch.Common.Event;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CityWatch.Server.Services
{
    public class ServiceManager : IHostedService
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public static ConcurrentQueue<Message> MessageQueue = new ConcurrentQueue<Message>();

        private Thread WorkerThread;
        private bool Running = false;

        public IDatabaseService DatabaseService { get; private set; }

        List<TechnicalDevice> TechnicalDevices = new List<TechnicalDevice>();
        List<DeviceBase> Devices = new List<DeviceBase>();

        IHubContext<DeviceHub, IDeviceHub> DeviceHubContext;

        public ServiceManager(IDatabaseService databaseService, IHubContext<DeviceHub, IDeviceHub> deviceHubCtx)
        {
            DatabaseService = databaseService;
            DeviceHubContext = deviceHubCtx;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            log.Info("Starting ServiceManager...");

            try
            {
                var ctx = DatabaseService.GetContext();

                TechnicalDevices.AddRange(await ctx.QueryAsync<TechnicalDevice>("SELECT * FROM TechnicalDevice"));
                foreach (var device in TechnicalDevices)
                {
                    if (device.DeviceType == "DigitalDevice")
                    {
                        Devices.Add(new DigitalDevice(device.GetAttributeValue("DeviceId"), device.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error loading server collections");
            }

            Running = true;
            WorkerThread = new Thread(Worker);
            WorkerThread.Start();

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.Info("Stopping ServiceManager...");

            Running = false;
            WorkerThread?.Join();

            return Task.CompletedTask;
        }

        private void Worker()
        {
            while (Running)
            {
                if (MessageQueue.TryDequeue(out Message message))
                {
                    switch (message.MessageType)
                    {
                        case Message.EMessageType.Device:
                            ProcessDeviceMessage(message);
                            break;
                        default:
                            log.Warn("Unknown message type: " + message.MessageType);
                            break;
                    }
                }


                Thread.Sleep(100);
            }
        }

        private async void ProcessDeviceMessage(Message message)
        {
            log.Info("Processing message from device: " + message.DeviceId);

            var device = TechnicalDevices.FirstOrDefault(x => x.GetAttributeValue("DeviceId") == message.DeviceId);
            if (device == null)
            {
                log.Warn("Uknown device reported: " + message.DeviceId);
            }

            var deviceEvent = new DigitalDeviceEvent()
            {
                IdTechnicalDevice = device != null ? device.IdTechnicalDevice : 0,
                Description = ParseDigitalDeviceMessage(device, message),
                EventDateTimeUTC = DateTime.UtcNow,
                Source = device != null ? device.Name : "CityWatch"
            };

            //DispatchEvent(await SaveEvent(deviceEvent));
        }

        private string ParseDigitalDeviceMessage(TechnicalDevice device, Message message)
        {
            if (device == null)
            {
                return "Unknown device: " + message.RawData;
            }

            return $"Device state has changed to { message.State }";
        }

        private void DispatchEvent(TechnicalEvent e)
        {
            if (e == null) return;

            DeviceHubContext.Clients.All.BroadcastEvent(e);
        }

        private void DispatchEvent(TechnicalDevice device, Message m)
        {
            if (device == null || m == null) return;

            DeviceHubContext.Clients.All.BroadcastMessage(device, m);
        }

        private async Task<TechnicalEvent> SaveEvent(TechnicalEvent e)
        {
            using (var ctx = DatabaseService.GetContext())
            {
                if (e.IdTechnicalEvent == 0)
                {
                    var result = await ctx.InsertAsync(e);
                    if (result != 0)
                    {
                        e.IdTechnicalEvent = result;
                    }
                    else
                    {
                        log.Error("Database error!");
                        e = null;
                    }
                }
                else
                {
                    if (!await ctx.UpdateAsync(e))
                    {
                        log.Error("Database error!");
                        e = null;
                    }
                }
            }

            return e;
        }
    }
}
