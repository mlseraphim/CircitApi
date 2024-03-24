using CircitApi.Infrastructure.Models;
using CircitApi.Models;
using CircitApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CircitApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronomyController : ControllerBase
    {
        private readonly RapidApiSettings RapidApiSettings;
        private readonly IRapidApi RapidApi;


        public AstronomyController(IOptions<RapidApiSettings> rapidApiSettings, IRapidApi rapidApi)
        {
            RapidApiSettings = rapidApiSettings.Value;
            RapidApi = rapidApi;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string cityName)
        {
            string resourceLocation = RapidApiSettings.AstronomyResource;

            var result = await RapidApi.GetEndPoint<RapidAstronomy>(resourceLocation, cityName);

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