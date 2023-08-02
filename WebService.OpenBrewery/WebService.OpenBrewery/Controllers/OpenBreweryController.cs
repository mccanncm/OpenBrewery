using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace WebService.OpenBrewery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenBreweryController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<OpenBreweryController> _logger;

        public OpenBreweryController(ILogger<OpenBreweryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Brewery")]
        public async Task<Brewery> Get()
        {
            Brewery brewery = new Brewery();
            //https://api.openbrewerydb.org/v1/breweries/b54b16e1-ac3b-4bff-a11f-f7ae9ddc27e0"
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://api.openbrewerydb.org/v1/breweries/b54b16e1-ac3b-4bff-a11f-f7ae9ddc27e0");
                response.EnsureSuccessStatusCode();
                string responseBosy = await response.Content.ReadAsStringAsync();
                brewery = String.IsNullOrEmpty(responseBosy) ? brewery : JsonSerializer.Deserialize<Brewery>(responseBosy);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }

            return brewery;
        }
    }
}