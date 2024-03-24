using CircitApi.Infrastructure.Enums;
using CircitApi.Infrastructure.Helpers;

namespace CircitApi.Infrastructure.Services
{
    public class EventLogger : IEventLogger
    {
        private IEventLoggerHelpers EventLoggerHelpers;


        public EventLogger(IEventLoggerHelpers eventLoggerHelpers)
        {
            EventLoggerHelpers = eventLoggerHelpers;
        }


        public void WriteErrorEvent(string namespaceClass, string methodInfo, Exception ex, int errorNumber)
        {
            var logMessage = ex.Message + ex.StackTrace;
            var innerException = ex.InnerException != null ? ex.InnerException.Message : "";

            EventLoggerHelpers.WriteLog(LogEventType.Error, namespaceClass, methodInfo, logMessage, innerException, errorNumber);
        }

        public void WriteErrorEvent(string namespaceClass, string methodInfo, string exceptionMessage, int errorNumber)
        {
            EventLoggerHelpers.WriteLog(LogEventType.Error, namespaceClass, methodInfo, exceptionMessage, null, errorNumber);
        }
    }
}