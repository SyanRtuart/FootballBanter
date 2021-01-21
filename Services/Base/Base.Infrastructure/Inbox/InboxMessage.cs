using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Inbox
{
    public class InboxMessage
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public InboxMessage(Guid id, DateTime occurredOn, string type, string data)
        {
            Id = id;
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }
    }
}
