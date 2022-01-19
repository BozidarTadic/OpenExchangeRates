using Microsoft.AspNetCore.Mvc;
using OpenExchangeRates.Api.BL.Interfaces;
using OpenExchangeRates.Api.Models;


namespace OpenExchangeRates.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;
        private readonly ICurrencyService _service;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                        HttpClient httpClient,
                                        ICurrencyService service)
        {
            _logger = logger;
            _httpClient = httpClient;
            _service = service;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
       [HttpGet]
       [Route("Api")]
       public IActionResult Api()
        {
            
            var url = _httpClient.BaseAddress + "latest.json?app_id=769ecacefa2141d69a2212a60b60a395";
            var result = _httpClient.GetAsync(url).Result.Content.ReadFromJsonAsync<CurrencyDto>();
            if (result.IsCompletedSuccessfully)
            {
                return Ok(result);
            }
            return BadRequest(); 
                
        }
        [HttpGet]
        [Route("NBS")]
        public IActionResult NBS()
        {
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            var urlUsd = "https://kurs.resenje.org/api/v1/currencies/Usd/rates/"+date;
            var urlEur = "https://kurs.resenje.org/api/v1/currencies/eur/rates/2022-01-18";
            var urlGbr = "https://kurs.resenje.org/api/v1/currencies/gbr/rates/2022-01-18";
            
            var usd = _httpClient.GetAsync(urlUsd).Result.Content.ReadFromJsonAsync<NBSDto>();
            var eur = _httpClient.GetAsync(urlEur).Result.Content.ReadFromJsonAsync<NBSDto>();
            var result = usd.Result.exchange_middle / eur.Result.exchange_middle;

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        [HttpGet]
        [Route("Result")]
        public IActionResult Result()
        {
            var result = _service.GetAvg();
            return Ok(result);
        }
    }
}