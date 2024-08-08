using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager() { }
        
        public void LogDebugging(string message) => logger.Debug(message);
        public void LogError(string mesg) => logger.Error(mesg);
        public void LogInfo(string msg) => logger.Info(msg);
        public void LogWarning(string msg) => logger.Warn(msg);
    }
}
