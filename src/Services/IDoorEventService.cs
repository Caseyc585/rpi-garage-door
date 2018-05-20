using System.Threading.Tasks;

namespace rpi_garage_door
{
    public interface IDoorEventService
    {
        Task PostDoorServer(DoorEventBody body);
    }
}