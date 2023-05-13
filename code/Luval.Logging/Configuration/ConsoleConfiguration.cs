using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Configuration
{
    public class ConsoleConfiguration : BasicConfig
    {
        public ConsoleConfiguration() : base() 
        {
            Colors = new Dictionary<LogLevel, ConsoleColor>(){
                { LogLevel.None, ConsoleColor.White },
                { LogLevel.Debug, ConsoleColor.Magenta },
                { LogLevel.Trace, ConsoleColor.Magenta },
                { LogLevel.Information, ConsoleColor.White },
                { LogLevel.Warning, ConsoleColor.Yellow },
                { LogLevel.Error, ConsoleColor.Red },
                { LogLevel.Critical, ConsoleColor.Red }
            };
        }
        public Dictionary<LogLevel, ConsoleColor> Colors { get; set; }
    }
}
