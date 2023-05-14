using Luval.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Providers
{
    public class ColorConsoleLogger : BaseLogger
    {

        private ConsoleConfiguration _configuration;

        public ColorConsoleLogger() : this(new ConsoleConfiguration())
        {

        }

        public ColorConsoleLogger(ConsoleConfiguration configuration) : base(configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        protected override void DoLog(LogLevel logLevel, EventId eventId, Exception? exception, string message)
        {
            WriteLine(logLevel, message);
        }

        private void WriteLine(LogLevel logLevel, string message)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = _configuration.Colors[logLevel];
            Console.WriteLine(message);
            Console.ForegroundColor = c;
        }
    }
}
