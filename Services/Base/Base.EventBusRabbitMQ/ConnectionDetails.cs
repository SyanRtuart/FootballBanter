using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.EventBusRabbitMQ
{
    public class ConnectionDetails
    {
        public string EventBusConnection { get; set; }
        public int EventBusRetryCount { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
        public int EventBusPort { get; set; }
    }
}
