using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);
    }
}
