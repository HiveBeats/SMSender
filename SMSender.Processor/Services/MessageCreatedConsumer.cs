using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SMSender.Shared.Dto;

namespace SMSender.Processor.Services
{
    public class MessageCreatedConsumer: IConsumer<ShortMessageCreated>
    {
        private readonly IShortMessageProcessingService _service;
        private readonly ILogger<MessageCreatedConsumer> _logger;
        
        public MessageCreatedConsumer(IShortMessageProcessingService service, ILogger<MessageCreatedConsumer> logger)
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