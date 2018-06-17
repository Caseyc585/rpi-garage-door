using System;
using Microsoft.AspNetCore.Mvc;
using Bifrost.Devices.Gpio.Core;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio;
using Microsoft.Extensions.Logging;
using rpi_garage_door.Services;

namespace rpi_garage_door.Controllers
{
    [Route("api/[controller]")]
    public class PinsController : Controller
    {
        private readonly IGpioController _gpioController;
        private readonly IPinService _pinService;
        private readonly ILogger<PinsController> _logger;

        public PinsController(IGpioController gpioController, ILogger<PinsController> logger, IPinService pinService)
        {
            _logger = logger;
            _gpioController = gpioController;
            _pinService = pinService;
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
            _pinService.WritePin(pinId, (GpioPinValue)status);
        }
    }
}