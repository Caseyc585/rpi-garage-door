using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public interface IPinCheckerService
    {
        int CheckPin(int pinId);
    }
}