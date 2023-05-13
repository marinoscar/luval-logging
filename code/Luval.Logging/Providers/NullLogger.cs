using Luval.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Providers
{
    public class NullLogger : BaseLogger
    {
        public NullLogger() : this(new BasicConfig() { LogVerbosity = LogVerbosity.None })
        {

        }

        public NullLogger(BasicConfig config) : base(config)
        {

        }

        protected override void DoLog(LogLevel logLevel, EventId eventId, Exception? exception, string message)
        {
            Debug.WriteLine(message);
        }

    }
}
