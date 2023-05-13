using Luval.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Providers
{
    public abstract class BaseLogger : ILogger
    {
        private BasicConfig _config;

        protected BaseLogger(BasicConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _config = config;
        }

        /// <inheritdoc/>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        /// <inheritdoc/>
        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return _config.LogLevels.Contains(logLevel);
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            DoLog(logLevel, eventId, exception,_config.Format(logLevel, eventId, exception, formatter(state, exception)));
        }

        protected abstract void DoLog(LogLevel logLevel, EventId eventId, Exception? exception, string message);


    }
}
