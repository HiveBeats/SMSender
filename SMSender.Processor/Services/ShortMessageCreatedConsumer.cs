using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SMSender.Shared.Dto;

namespace SMSender.Processor.Services
{
    public class ShortMessageCreatedConsumer: IConsumer<ShortMessageCreated>
    {
        private readonly IShortMessageProcessingService _service;
        private readonly ILogger<ShortMessageCreatedConsumer> _logger;
        
        public ShortMessageCreatedConsumer(IShortMessageProcessingService service, ILogger<ShortMessageCreatedConsumer> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        public async Task Consume(ConsumeContext<ShortMessageCreated> context)
        {
            var message = context.Message;
            await _service.SendShortMessage(message);
        }
    }
}