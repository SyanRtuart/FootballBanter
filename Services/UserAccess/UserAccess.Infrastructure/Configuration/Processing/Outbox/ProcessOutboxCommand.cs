using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.Contracts;

namespace UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase, IRecurringCommand
    {

    }
}
