using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.SDK.Enumerations;
using PortfolioAPI.SDK.Services;
using System.Text.Json;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VantageController : ControllerBase
    {
        private readonly ILogger<VantageController> _logger;
        private readonly VantageAPIService _vantageService;

        public VantageController(
            ILogger<VantageController> logger,
            VantageAPIService vantageService
            )
        {
            _logger = logger;
            _vantageService = vantageService;
        }

        [HttpGet()]
        [Route("{symbol}/{verb:VantageVerbs}")]
        public async Task<JsonResult> Get([FromRoute] string symbol, VantageVerbs verb)
        {
            List<KeyValuePair<string, dynamic>> result = await _vantageService
                .QueryAlphaVantage(verb, 
                    new StockFilterDTO() { 
                        Symbol = symbol
                    }
                );

            return new JsonResult(result);
        }
    }
}