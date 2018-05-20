using System.Threading.Tasks;

namespace rpi_garage_door.Services
{
    public interface IDoorMonitoringService
    {
        Task PerformCheck();
    }
}