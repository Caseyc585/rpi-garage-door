using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public class EventHubProcessorFactory : IEventProcessorFactory
    {
        private readonly ILogger _logger;
        private readonly IDoorEventService _doorEventSvc;

        public EventHubProcessorFactory(ILogger logger, IDoorEventService doorEventService)
        {
            _logger = logger;
            _doorEventSvc = doorEventService;
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new EventHubProcessor(_logger, _doorEventSvc);
        }
    }
}
