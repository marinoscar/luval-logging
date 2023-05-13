using Microsoft.Extensions.Logging;

namespace Luval.Logging.Configuration
{
    public class BasicConfig
    {

        private LogVerbosity _verbosity;

        public BasicConfig()
        {
            SetVerbosity(LogVerbosity.All);
            Format = DoFormat;
        }

        /// <summary>
        /// Gets the message formatting
        /// </summary>
        public Func<LogLevel, EventId, Exception?, string, string>? Format { get; set; }
        /// <summary>
        /// Gets the <see cref="LogLevels"/> enabled
        /// </summary>
        public List<LogLevel> LogLevels { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LogVerbosity"/> for the <see cref="ILogger"/>
        /// </summary>
        public LogVerbosity LogVerbosity
        {
            get { return _verbosity; }
            set { SetVerbosity(value); }
        }

        protected virtual string DoFormat(LogLevel logLevel, EventId eventId, Exception? exception, string message)
        {
            return exception == null ? $"{DateTime.UtcNow:s} [{logLevel}] ({eventId}) {message}"
                : $"{DateTime.UtcNow:s} [{logLevel}] ({eventId}) {exception}";
        }

        /// <summary>
        /// Sets the verbosity level for the <see cref="ILogger"/>
        /// </summary>
        /// <param name="logVerbosity"></param>
        public void SetVerbosity(LogVerbosity logVerbosity)
        {
            _verbosity = logVerbosity;
            switch (logVerbosity)
            {
                case LogVerbosity.None:
                    LogLevels = new List<LogLevel>();
                    break;
                case LogVerbosity.All:
                    LogLevels = new List<LogLevel> { LogLevel.None, LogLevel.Debug, LogLevel.Trace, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
                    break;
                case LogVerbosity.Information:
                    LogLevels = new List<LogLevel> { LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
                    break;
                case LogVerbosity.Warning:
                    LogLevels = new List<LogLevel> { LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
                    break;
                case LogVerbosity.Error:
                    LogLevels = new List<LogLevel> { LogLevel.Error, LogLevel.Critical };
                    break;
                default:
                    LogLevels = new List<LogLevel> { LogLevel.Error, LogLevel.Critical };
                    break;
            }
        }
    }

    /// <summary>
    /// Identifies the level of verbosity
    /// </summary>
    public enum LogVerbosity
    {
        /// <summary>
        /// Do not provide any logging messages
        /// </summary>
        None,
        /// <summary>
        /// Logs all levels
        /// </summary>
        All,
        /// <summary>
        /// Logs <see cref="LogLevel.Information"/>, <see cref="LogLevel.Warning"/>, <see cref="LogLevel.Error"/>, <see cref="LogLevel.Critical"/>
        /// </summary>
        Information,
        /// <summary>
        /// Logs <see cref="LogLevel.Warning"/>, <see cref="LogLevel.Error"/>, <see cref="LogLevel.Critical"/>
        /// </summary>
        Warning,
        /// <summary>
        /// Logs <see cref="LogLevel.Error"/>, <see cref="LogLevel.Critical"/>
        /// </summary>
        Error
    }
}