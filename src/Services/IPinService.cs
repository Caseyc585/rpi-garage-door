using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Bifrost.Devices.Gpio.Core;

namespace rpi_garage_door.Services
{
    public interface IPinService
    {
        int CheckPin(int pinId);
        bool WritePin(int pinId, GpioPinValue pinValue);
    }
}