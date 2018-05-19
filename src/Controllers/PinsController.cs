using System;
using Microsoft.AspNetCore.Mvc;
using Bifrost.Devices.Gpio.Core;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Controllers
{
    [Route("api/[controller]")]
    public class PinsController : Controller
    {
        private readonly IGpioController _gpioController;
        private readonly ILogger<PinsController> _logger;

        public PinsController(IGpioController gpioController, ILogger<PinsController> logger)
        {
            _logger = logger;
            _gpioController = gpioController;
        }

        // GET api/pins
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("About to list pin statuses.");
            return Ok(_gpioController.Pins);
        }

        // GET api/pins/5
        [HttpGet("{pinId}")]
        public IActionResult Get(int pinId)
        {
            GpioPinValue pinStatus;

            _logger.LogInformation("About to get pin status.");
            var pin = _gpioController.OpenPin(pinId);

            pinStatus = pin.Read();

            _logger.LogInformation("Returning pin status.");
            return Ok(pinStatus.ToString());
        }

        // POST api/pins
        [HttpPost]
        public void SwitchPin(int pinId, int status)
        {
            _logger.LogInformation("About to change pin status.");
            var pin = _gpioController.OpenPin(pinId);

            pin.SetDriveMode(GpioPinDriveMode.Output);

            if (status == 1)
            {
                _logger.LogInformation("Going on");
                pin.Write(GpioPinValue.High);
            }
            else
            {
                _logger.LogInformation("Going off");
                pin.Write(GpioPinValue.Low);
            }
        }
    }
}