using System.Collections.Generic;

namespace rpi_garage_door.Models
{
    public class DoorEventHubSettings
    {
        public string EventHubEntityPath { get; set; }
        public string EventHubConnectionString { get; set; }
        public string StorageConnectionString { get; set; }
        public string StorageContainerName { get; set; }
    }
}