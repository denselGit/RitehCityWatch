using CityWatch.Common;
using CityWatch.Common.Event;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CityWatch.Server.Services
{
    public interface IDeviceHub
    {
        Task BroadcastMessage(TechnicalDevice device, Message msg);
        Task BroadcastEvent(TechnicalEvent e);
    }

    public class DeviceHub : Hub<IDeviceHub>
    {
    }
}
