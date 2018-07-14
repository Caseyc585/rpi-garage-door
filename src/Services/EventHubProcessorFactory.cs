using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public class EventHubProcessorFactory : IEventProcessorFactory
    {
        private readonly ILogger _logger;

        public EventHubProcessorFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new EventHubProcessor(_logger);
        }
    }
}