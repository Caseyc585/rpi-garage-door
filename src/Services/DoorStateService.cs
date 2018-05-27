using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using rpi_garage_door.Models;

namespace rpi_garage_door
{
    public class DoorStateService:IDoorStateService
    {
        private Dictionary<int, DoorState> _doors { get; set; }

        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;

        public DoorStateService(IOptions<AppSettings> options, ILogger<DoorStateService> logger)
        {
            _logger = logger;
            _appSettings = options.Value;
            _doors = new Dictionary<int, DoorState>();
        }

        public DoorState GetState(int id)
        {
            if (!_doors.ContainsKey(id))
                _doors.Add(id, DoorState.New);


            return _doors[id];
        }

        public void SetState(int id, DoorState state)
        {
            _doors[id] = state;
        }
    }
}