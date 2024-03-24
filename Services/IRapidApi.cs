namespace CircitApi.Services
{
    public interface IRapidApi
    {
        Task<T?> GetEndPoint<T>(string loc, string cityName);
    }
}