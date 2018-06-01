using System.Threading.Tasks;
using Bifrost.Devices.Gpio.Core;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace rpi_garage_door.Services
{
    public class PinCheckerService:IPinCheckerService
    {
        private readonly IGpioController _gpioController;
        private readonly ILogger<PinCheckerService> _logger;

        public PinCheckerService(IGpioController gpioController, 
                                ILogger<PinCheckerService> logger)
        {
            _logger = logger;
            _gpioController = gpioController;
        }

        public int CheckPin(int pinId)
        {
            GpioPinValue pinStatus;

            _logger.LogInformation("About to get pin status:" + pinId);
            var pin = _gpioController.OpenPin(pinId);

            pinStatus = pin.Read();

            _logger.LogInformation("Returning pin status.");
            _logger.LogInformation(pinStatus.ToString());
            return (int)pinStatus;
        }
    }
}