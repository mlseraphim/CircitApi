namespace CircitApi.Infrastructure.Services
{
    public interface IEventLogger
    {
        void WriteErrorEvent(string namespaceClass, string methodInfo, Exception ex, int errorNumber);

        void WriteErrorEvent(string namespaceClass, string methodInfo, string exceptionMessage, int errorNumber);
    }
}