using CircitApi.Infrastructure.Models;
using CircitApi.Models;
using CircitApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CircitApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimezoneController : ControllerBase
    {
        private readonly RapidApiSettings RapidApiSettings;
        private readonly IRapidApi RapidApi;


        public TimezoneController(IOptions<RapidApiSettings> rapidApiSettings, IRapidApi rapidApi)
        {
            RapidApiSettings = rapidApiSettings.Value;
            RapidApi = rapidApi;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string cityName)
        {
            string resourceLocation = RapidApiSettings.TimezoneResource;

            var result = await RapidApi.GetEndPoint<RapidTimezone>(resourceLocation, cityName);

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