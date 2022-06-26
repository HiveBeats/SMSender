using System;
using System.Collections.Generic;

namespace SMSender.Api.Dto
{
    public class SendMessageRequest
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Content { get; set; }
    }
}