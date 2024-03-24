using CircitApi.Infrastructure.Models;
using CircitApi.Models;
using CircitApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CircitApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly RapidApiSettings RapidApiSettings;
        private readonly IRapidApi RapidApi;


        public WeatherController(IOptions<RapidApiSettings> rapidApiSettings, IRapidApi rapidApi)
        {
            RapidApiSettings = rapidApiSettings.Value;
            RapidApi = rapidApi;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string cityName)
        {
            string resourceLocation = RapidApiSettings.WeatherResource;

            var result = await RapidApi.GetEndPoint<RapidWeather>(resourceLocation, cityName);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}