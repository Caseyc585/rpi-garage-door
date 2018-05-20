using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public class PinCheckerService:IPinCheckerService
    {
        private readonly IGpioController _gpioController;
        private readonly ILogger<PinsController> _logger;
        private readonly PinSettings _pinSettings;

        public PinCheckerService(IGpioController gpioController, 
                                IOptions<PinSettings> pinOptions, 
                                ILogger<PinsController> logger)
        {
            _logger = logger;
            _gpioController = gpioController;
            _pinSettings = pinSettings.Value;
        }

        public void CheckPins()
        {
            foreach (var pin in _pinSettings.PinsToWatch)
            {
                CheckPin(pin);
            }
        }

        public void CheckPin(int pindId)
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