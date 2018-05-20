using System.Collections.Generic;
using rpi_garage_door.Models;

namespace rpi_garage_door
{
    public interface IDoorStateService
    {
        DoorState GetState(int id);
        void SetState(int id, DoorState state);
    }
}