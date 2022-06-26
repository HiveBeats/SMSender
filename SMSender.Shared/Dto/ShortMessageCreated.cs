using System.Collections.Generic;

namespace SMSender.Shared.Dto
{
    public interface IShortMessageDto
    {
        string From { get; }
        List<string> To { get; }
        string Content { get; }
    }
    public class ShortMessageCreated:IShortMessageDto
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Content { get; set; }
    }
}