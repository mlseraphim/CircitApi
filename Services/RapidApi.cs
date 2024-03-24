using System.Text.Json;

namespace CircitApi.Services
{
    public class RapidApi : IRapidApi
    {
        private readonly IHttpClientFactory HttpClientFactory;


        public RapidApi(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }


        public async Task<T?> GetEndPoint<T>(string loc, string cityName)
        {
            var httpClient = HttpClientFactory.CreateClient("RapidApi");
            var httpResponseMessage = await httpClient.GetAsync(loc + cityName);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<T>(contentStream);
            }

            return default;
        }
    }
}