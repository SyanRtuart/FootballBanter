using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    public class InboxMessageDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }
    }
}
