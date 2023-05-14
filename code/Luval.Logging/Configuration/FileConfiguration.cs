using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Configuration
{
    public class FileConfiguration : BasicConfig
    {

        public FileConfiguration()
        {
            DirectoryName = Environment.CurrentDirectory;
            MaxLogFileSize = 1024 * 1024;
            FileName = "Events.log";
            TotalFilesInDirectory = 10;
        }

        /// <summary>
        /// Gets or sets the full name of the directory where the logs will be located
        /// </summary>
        public string? DirectoryName { get; set; }

        /// <summary>
        /// Gets or sets the max size of the log file in bytes
        /// </summary>
        public long? MaxLogFileSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how many log files will be kept at any given time, default to 10
        /// </summary>
        public int TotalFilesInDirectory { get; set; }

        /// <summary>
        /// Gets or sets the file name
        /// </summary>
        public string ? FileName { get; set; }


    }
}
