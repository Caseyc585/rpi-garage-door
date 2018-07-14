using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using rpi_garage_door;
using rpi_garage_door.Models;
using rpi_garage_door.Services;

public class DoorQueueService :IHostedService
{
    public DoorQueueService(IOptions<AppSettings> options, 
                            ILogger<DoorQueueService> logger,
                            IDoorEventService doorEventService)
    {
        _logger = logger;
        _doorEventSvc = doorEventService;
        _hubSettings = options.Value.DoorEventHubSettings;
    }
    private EventProcessorHost _eventProcessorHost;
    private ILogger<DoorQueueService> _logger;
    private readonly IDoorEventService _doorEventSvc;
    private readonly DoorEventHubSettings _hubSettings;
    

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _eventProcessorHost = new EventProcessorHost(
        _hubSettings.EventHubEntityPath,
        PartitionReceiver.DefaultConsumerGroupName,
        _hubSettings.EventHubConnectionString,
        _hubSettings.StorageConnectionString,
        _hubSettings.StorageContainerName);

        // Registers the Event Processor Host and starts receiving messages
        await _eventProcessorHost
            .RegisterEventProcessorFactoryAsync(new EventHubProcessorFactory(_logger, _doorEventSvc));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _eventProcessorHost.UnregisterEventProcessorAsync();
    }
}