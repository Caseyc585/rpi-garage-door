using System.Threading.Tasks;
using rpi_garage_door.Models;

namespace rpi_garage_door
{
    public interface IDoorEventService
    {
        Task PostDoorServer(DoorEventBody body);
    }
}