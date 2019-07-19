using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace AspNetCoreProject.API.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _path;
        private Object _cap = new Object();
        
        public FileLogger(string path) {
            _path = path;
        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Log level: {logLevel}");
            stringBuilder.Append($"EventId: {eventId}");
            stringBuilder.Append($"State: {state}");
            stringBuilder.Append($"Exception: {exception.Message}");
            stringBuilder.Append(exception.Source);
            stringBuilder.Append(exception.StackTrace);

            lock (_cap) {
                var exists = Directory.Exists(_path);
                if (!exists)
                    Directory.CreateDirectory(_path);
                using (var streamWriter = new StreamWriter(_path + DateTime.Now.ToString("yy-MM-dd") + ".txt")) {
                    streamWriter.WriteLine(stringBuilder.ToString());
                }                
            }
        }

        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state) {
            return null;
        }
    }
}