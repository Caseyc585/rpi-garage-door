using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;


namespace rpi_garage_door.Services
{
    public class SchedulerService:IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly IDoorMonitoringService _doorMonitoringService;

        public SchedulerService(IDoorMonitoringService doorMonitoringService, ILogger<PinService> logger)
        {
            _logger = logger;
            _doorMonitoringService = doorMonitoringService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            
            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {   
            _logger.LogInformation("Running " + DateTime.Now.ToString());
            _doorMonitoringService.PerformCheck();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}