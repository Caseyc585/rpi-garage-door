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
using rpi_garage_door.Models;
using rpi_garage_door.Services;

public class DoorQueueService :IHostedService
{
    public DoorQueueService(IOptions<AppSettings> options, ILogger<DoorQueueService> logger)
    {
        _logger = logger;
    }
    private EventProcessorHost _eventProcessorHost;
    private ILogger<DoorQueueService> _logger;
    private DoorEventHubSettings hubSettings;
    

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _eventProcessorHost = new EventProcessorHost(
        hubSettings.EventHubEntityPath,
        PartitionReceiver.DefaultConsumerGroupName,
        hubSettings.EventHubConnectionString,
        hubSettings.StorageConnectionString,
        hubSettings.StorageContainerName);

        // Registers the Event Processor Host and starts receiving messages
        await _eventProcessorHost.RegisterEventProcessorFactoryAsync(new EventHubProcessorFactory(_logger));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _eventProcessorHost.UnregisterEventProcessorAsync();
    }
}