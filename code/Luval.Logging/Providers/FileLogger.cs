using Luval.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Logging.Providers
{
    public class FileLogger : BaseLogger
    {

        private FileConfiguration _configuration;
        private FileInfo _file;
        private DateTime? _utcCleanCheck;

        public FileLogger() : this(new FileConfiguration())
        {
            
        }

        public FileLogger(FileConfiguration configuration) : base(configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _file = new FileInfo(Path.Combine(_configuration.DirectoryName, _configuration.FileName));
        }

        protected override void DoLog(LogLevel logLevel, EventId eventId, Exception? exception, string message)
        {
            File.AppendAllText(GetFile().FullName, message);
            ValidateCleanUp();
        }

        private FileInfo GetFile()
        {
            if (!_file.Exists) return _file;
            var files = GetAllFiles();
            var workingFile = files.FirstOrDefault();
            if(workingFile == null) return _file;
            if (workingFile.Length > _configuration.MaxLogFileSize)
                return new FileInfo(Path.Combine(_configuration.DirectoryName,
                    _file.Name.Replace(_file.Extension, "") + files.Count.ToString().PadLeft(3, '0') + _file.Extension));
            return workingFile;


        }


        private void ValidateCleanUp()
        {
            if(_utcCleanCheck == null)
            {
                _utcCleanCheck = DateTime.UtcNow;
                CleanDirectoryAsync();
                return;
            }
            if(DateTime.UtcNow.Subtract(_utcCleanCheck.Value).TotalHours > 4)
            {
                CleanDirectoryAsync();
                _utcCleanCheck = DateTime.UtcNow;
            }
        }

        private string GetNamePattern()
        {
            return _file.Name.Replace(_file.Extension, "") + "*" + _file.Extension;
        }

        public List<FileInfo> GetAllFiles()
        {
            var dirInfo = new DirectoryInfo(_configuration.DirectoryName);
            return dirInfo.GetFiles(GetNamePattern(), SearchOption.TopDirectoryOnly).OrderByDescending(i => i.CreationTimeUtc).ToList();
        }

        public Task CleanDirectoryAsync()
        {
            return Task.Run(() => { 
                var files = GetAllFiles().Skip(_configuration.TotalFilesInDirectory).ToList();
                foreach (var file in files)
                {
                    file.Delete();
                }
            });
        }
    }
}
