using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenExchangeRates.Api.BL.Interfaces;

namespace OpenExchangeRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        
        private readonly HttpClient _httpClient;
        private readonly ICurrencyService _service;
        public CurrencyController(
                                        HttpClient httpClient,
                                        ICurrencyService service)
        {
            
            _httpClient = httpClient;
            _service = service;
        }
        [HttpGet]
        [Route("Result")]
        public IActionResult Result()
        {
            var result = _service.GetAvg();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
