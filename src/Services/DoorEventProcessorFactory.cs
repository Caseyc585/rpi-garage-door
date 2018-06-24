using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door.Services
{
    public class DoorEventProcessorFactory : IEventProcessorFactory
    {
        private readonly ILogger _logger;

        public DoorEventProcessorFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new DoorEventProcessor(_logger);
        }
    }
}
