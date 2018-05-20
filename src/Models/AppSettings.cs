using System.Collections.Generic;

namespace rpi_garage_door.Models
{
    public class AppSettings
    {
        public string DoorEventURL { get; set; }
        public List<DoorSetting> DoorSettings { get; set; }
    }
}