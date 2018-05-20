using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public class PinCheckerService:IPinCheckerService
    {
        private int[] pins = [1,2];

        private readonly IGpioController _gpioController;
        private readonly ILogger<PinsController> _logger;

        public PinCheckerService(IGpioController gpioController, ILogger<PinsController> logger)
        {
            _logger = logger;
            _gpioController = gpioController;
        }

        public void CheckPins()
        {
            foreach (var pin in pins)
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