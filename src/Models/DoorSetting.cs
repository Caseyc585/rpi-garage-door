namespace rpi_garage_door.Models
{
    public class DoorSetting
    {
        public int CloseSensorPin { get; set; }
        public int TriggerPin { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}