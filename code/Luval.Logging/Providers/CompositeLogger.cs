using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Providers
{
    public class CompositeLogger : ILogger
    {
        public List<ILogger> Loggers { get; private set; }

        public CompositeLogger(IEnumerable<ILogger> loggers)
        {
            if (loggers == null) throw new ArgumentNullException(nameof(loggers));
            Loggers = new List<ILogger>();
            Loggers.AddRange(loggers);

        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var tasks = new List<Task>();
            foreach (var log in Loggers)
            {
                tasks.Add(Task.Factory.StartNew(() => { log.Log(logLevel, eventId, state, exception, formatter); }));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
