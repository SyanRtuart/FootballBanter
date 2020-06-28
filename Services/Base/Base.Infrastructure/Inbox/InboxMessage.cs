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

        public DateTime Date { get; set; }

        public DateTime ProcessedDate { get; set; }

        public InboxMessage(Guid id, DateTime occurredOn, string type, DateTime date, DateTime processedDate)
        {
            Id = id;
            OccurredOn = occurredOn;
            Type = type;
            Date = date;
            ProcessedDate = processedDate;
        }
    }
}
