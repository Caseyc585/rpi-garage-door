using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rpi_garage_door;
using rpi_garage_door.Models;

public class EventHubProcessor : IEventProcessor
{
    private readonly ILogger _logger;
    private readonly IDoorEventService _doorEventSvc;

    public EventHubProcessor(ILogger logger, IDoorEventService doorEventService)
    {
        _logger = logger;
        _doorEventSvc = doorEventService;
    }

    public Task CloseAsync(PartitionContext context, CloseReason reason)
    {
        _logger.LogInformation($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
        return Task.CompletedTask;
    }

    public Task OpenAsync(PartitionContext context)
    {
        _logger.LogInformation($"DoorEventProcessor initialized. Partition: '{context.PartitionId}'");
        return Task.CompletedTask;
    }

    public Task ProcessErrorAsync(PartitionContext context, Exception error)
    {
        _logger.LogInformation($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
        return Task.CompletedTask;
    }

    public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
    {
        foreach (var eventData in messages)
        {
            var data = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
            _logger.LogInformation($"Message received. Partition: '{context.PartitionId}', Data: '{data}'");

            try
            {
                var doorEventBody = JsonConvert.DeserializeObject<DoorEventBody>(data);
                _doorEventSvc.ToggleDoorEvent(doorEventBody);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error occured in processing event");
            }

        }

        return context.CheckpointAsync();
    }
}