using System.Collections.Generic;

namespace SMSender.Api.Dto
{
    public class ShortMessageDto
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}