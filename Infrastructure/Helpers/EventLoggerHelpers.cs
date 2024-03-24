using CircitApi.Infrastructure.Enums;
using CircitApi.Infrastructure.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CircitApi.Infrastructure.Helpers
{
    public class EventLoggerHelpers : IEventLoggerHelpers
    {
        private string LogArea { get; }
        private IEventLogWriter EventLogWriter { get; }


        public EventLoggerHelpers(IOptionsMonitor<AppSettings> optMonAppSettings, IEventLogWriter eventLogWriter)
        {
            LogArea = optMonAppSettings.CurrentValue.EventLogArea;
            EventLogWriter = eventLogWriter;
        }


        public void WriteLog(LogEventType logEventType, string namespaceClass, string methodInfo, string message, string innerExceptionMessage, int errorNumber)
        {
            try
            {
                string msgType = "";

                switch (logEventType)
                {
                    case LogEventType.Error:
                        msgType = "Error in";
                        break;

                    case LogEventType.Information:
                        msgType = "Information";
                        break;

                    case LogEventType.Warning:
                        msgType = "Warning logged in";
                        break;

                    default:
                        msgType = "Event Logged";
                        break;
                }

                var innerException = (string.IsNullOrEmpty(innerExceptionMessage)) ? "" : string.Format("\n\n{0}", innerExceptionMessage);

                var fullMessage = string.Format("{0} [{1}.{2}]: {3}", msgType, namespaceClass, methodInfo, message) + innerException;

                EventLogWriter.WriteEntry(LogArea, fullMessage, logEventType, errorNumber);
            }
            catch (Exception ex)
            {
                Debug.Write($"Error in EventLoggerHelpers WriteLog. {ex.Message}");
            }
        }
    }

    public interface IEventLogWriter
    {
        void WriteEntry(string LogArea, string fullMessage, LogEventType logEventType, int errorNumber);
    }

    public class EventLogWriter : IEventLogWriter
    {
        public void WriteEntry(string LogArea, string fullMessage, LogEventType logEventType, int errorNumber)
        {
            try
            {
                EventLogEntryType eventLogEntryType = EventLogEntryType.Information;

                switch (logEventType)
                {
                    case LogEventType.Error:
                        eventLogEntryType = EventLogEntryType.Error;
                        break;

                    case LogEventType.Warning:
                        eventLogEntryType = EventLogEntryType.Warning;
                        break;
                }

                EventLog.WriteEntry(LogArea, fullMessage, eventLogEntryType, errorNumber);
            }
            catch (Exception ex)
            {
                Debug.Write($"Couldn't write error to event log: check pemissions {ex.Message}");
                Debug.Write($"Error was: {LogArea}, {fullMessage}, {logEventType}, {errorNumber}");
            }
        }
    }
}