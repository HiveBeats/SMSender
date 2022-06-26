using System;
using System.Collections.Generic;

namespace SMSender.Shared.Models
{
    public class ShortMessage
    {
        protected ShortMessage() {}

        public ShortMessage(string from, IEnumerable<string> to, string content, ShortMessageStatus status = ShortMessageStatus.Delivered)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            Content = content;
            Status = status;
        }
        public string Id { get; set; }
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public string Content { get; set; }
        public ShortMessageStatus Status { get; set; }
    }
}