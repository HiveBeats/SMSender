using System;
using System.Threading.Tasks;
using PhoneNumbers;
using SMSender.Shared.Configuration;
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
        private readonly PhoneNumberUtil _phoneNumberUtil;
        public ShortMessageProcessingService(AppDbContext db)
        {
            _db = db;
            _phoneNumberUtil = PhoneNumberUtil.GetInstance();
        }

        public bool ValidatePhoneNumber(string number)
        {
            try
            {
                PhoneNumber phone = _phoneNumberUtil.Parse(number, Constants.AustraliaCountryCode);
                return _phoneNumberUtil.IsValidNumberForRegion(phone, Constants.AustraliaCountryCode);
            }
            catch (Exception e)
            {
                return false;
            }
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