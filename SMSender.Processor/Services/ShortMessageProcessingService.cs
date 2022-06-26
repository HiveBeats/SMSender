using System;
using System.Threading.Tasks;
using SMSender.Shared.Dto;
using SMSender.Shared.Models;

namespace SMSender.Processor.Services
{
    public interface IShortMessageProcessingService
    {
        bool ValidatePhoneNumber(string phone);
        Task SendShortMessage(IShortMessageDto message);
    }
    public class ShortMessageProcessingService: IShortMessageProcessingService
    {
        private readonly AppDbContext _db;

        public ShortMessageProcessingService(AppDbContext db)
        {
            _db = db;
        }

        public bool ValidatePhoneNumber(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task SendShortMessage(IShortMessageDto message)
        {
            ShortMessageStatus status = ShortMessageStatus.Delivered;
            
            if (!ValidatePhoneNumber(message.From))
                status = ShortMessageStatus.Failed;
            
            var item = new ShortMessage(message.From, message.To, message.Content, status);

            _db.ShortMessages.Add(item);
            await _db.SaveChangesAsync();
        }
    }
}