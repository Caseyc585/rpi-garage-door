using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public interface IPinService
    {
        int CheckPin(int pinId);
    }
}