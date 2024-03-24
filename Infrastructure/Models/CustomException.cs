namespace CircitApi.Infrastructure.Models
{
    public class CustomException : Exception
    {
        public string SourceName { get; set; }

        public string SourceMethod { get; set; }

        public Exception Exception { get; set; }

        public string ExceptionMessage { get; set; }

        public int ErrorNumber { get; set; }
    }
}