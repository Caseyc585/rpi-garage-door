using System.Threading.Tasks;
using Bifrost.Devices.Gpio.Core;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rpi_garage_door.Models;

namespace rpi_garage_door.Services
{
    public class PinCheckerService:IPinCheckerService
    {
        private readonly IGpioController _gpioController;
        private readonly ILogger<PinCheckerService> _logger;
        private readonly PinSettings _pinSettings;

        public PinCheckerService(IGpioController gpioController, 
                                IOptions<PinSettings> pinOptions, 
                                ILogger<PinCheckerService> logger)
        {
            _logger = logger;
            _gpioController = gpioController;
            _pinSettings = pinOptions.Value;
        }

        public void CheckPins()
        {
            foreach (var pin in _pinSettings.PinsToWatch)
            {
                CheckPin(pin);
            }
        }

        public void CheckPin(int pinId)
        {
            GpioPinValue pinStatus;

            _logger.LogInformation("About to get pin status.");
            var pin = _gpioController.OpenPin(pinId);

            pinStatus = pin.Read();

            _logger.LogInformation("Returning pin status.");
            _logger.LogInformation(pinStatus.ToString());
        }
    }
}