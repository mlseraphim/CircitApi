using CircitApi.Infrastructure.Enums;

namespace CircitApi.Infrastructure.Helpers
{
    public interface IEventLoggerHelpers
    {
        void WriteLog(LogEventType logEventType, string namespaceClass, string methodInfo, string message, string innerExceptionMessage, int errorNumber);
    }
}