using System.Collections.Generic;

namespace rpi_garage_door.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            DoorSettings = new List<DoorSetting>();
            DoorEventHubSettings = new DoorEventHubSettings();
        }
        public string DoorEventURL { get; set; }
        public List<DoorSetting> DoorSettings { get; set; }
        public string DoorQueueURL { get; set; }
        public DoorEventHubSettings DoorEventHubSettings { get; set; }
    }
}