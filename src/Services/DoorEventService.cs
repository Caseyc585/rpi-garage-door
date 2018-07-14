using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rpi_garage_door.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using rpi_garage_door.Services;
using Bifrost.Devices.Gpio.Core;
using System.Linq;

namespace rpi_garage_door
{
    public class DoorEventService : IDoorEventService
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly IPinService _pinService;

        public DoorEventService(IOptions<AppSettings> options, 
        ILogger<DoorEventService> logger,
        IPinService pinService)
        {
            _logger = logger;
            _appSettings = options.Value;
            _pinService = pinService;
        }

        public async Task PostDoorServer(DoorEventBody body)
        {
            using(HttpClient client = new HttpClient())
            {
                try	
                {
                    var json = JsonConvert.SerializeObject(body,
                    new JsonSerializerSettings 
                    { 
                        ContractResolver = new CamelCasePropertyNamesContractResolver() 
                    });
                    var content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync(_appSettings.DoorEventURL,  content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                }  
                catch(HttpRequestException e)
                {
                    _logger.LogError(e, "Error while post data");
                }
            }
        }

        public void ToggleDoorEvent(DoorEventBody body)
        {
            var door = _appSettings.DoorSettings.First(_ => _.Id == body.Door);
            var pin = door.TriggerPin;

            var state = _pinService.CheckPin(pin);

            if (state != (int)body.State)
            {
                string doorVerb = body.State == DoorState.Open ? "Opening" : "Closing";

                _logger.LogInformation(doorVerb + " Door...");
                _pinService.WritePin(pin, GpioPinValue.High);
                // Sleep for 2 sec
                System.Threading.Thread.Sleep(2000);
                _pinService.WritePin(pin, GpioPinValue.Low);
                _logger.LogInformation("Door has been triggered!");
            }
            else
            {
                string doorStateStr = body.State == DoorState.Open ? "Opened" : "Closed";

                _logger.LogInformation("Door is already " + doorStateStr);
            }
        }
    }
}