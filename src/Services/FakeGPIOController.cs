using System;
using System.Collections.Generic;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio;

namespace rpi_garage_door.Services
{
    public class FakeGPIOController : IGpioController
    {
        public IDictionary<string, string> Pins => new Dictionary<string, string>() 
        {
            { "", ""}
        };

        public IGpioPin OpenPin(int pinNumber)
        {
            return new GpioPin(pinNumber, "potato");
        }
    }
}