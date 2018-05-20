namespace rpi_garage_door.Models
{
    public class DoorEventBody
    {
        public DoorState State { get; set; }
        public int Door { get; set; }
    }
}