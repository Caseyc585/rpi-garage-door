using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using rpi_garage_door.Services;
using rpi_garage_door.Models;
using System;

namespace rpi_garage_door
{
    public class DoorMonitoringService:IDoorMonitoringService
    {
        public Dictionary<int, DoorState> Doors { get; set; }

        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly IPinService _pinCheckerService;
        private readonly IDoorStateService _doorStateService;
        private readonly IDoorEventService _doorEventService;

        public DoorMonitoringService(IPinService pinCheckerService, 
        IDoorStateService doorStateService,
        IDoorEventService doorEventService, 
        IOptions<AppSettings> options, 
        ILogger<DoorMonitoringService> logger)
        {
            _logger = logger;
            _appSettings = options.Value;
            _doorStateService = doorStateService;
            _doorEventService = doorEventService;
            _pinCheckerService = pinCheckerService;
        }

        public async Task PerformCheck()
        {
            try
            {
                var doorSettings = _appSettings.DoorSettings;

                foreach (var door in doorSettings)
                {
                    var pinStatus = _pinCheckerService.CheckPin(door.CloseSensorPin);

                    var state = _doorStateService.GetState(door.Id);

                    if (pinStatus != (int)state)
                    {
                        var newState = (DoorState)pinStatus;
                        var doorEvent = new DoorEventBody
                        {
                            State = newState,
                            Door = door.Id
                        };
                        await _doorEventService.PostDoorServer(doorEvent);

                        _doorStateService.SetState(door.Id, newState);
                    }
                    // else - ignore same state
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while performing check");
            }
        }
    }
}