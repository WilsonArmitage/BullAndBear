using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.External.Services;
using PortfolioAPI.SDK.DTO;
using PortfolioAPI.SDK.Enumerations;
using System.Text.Json;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VantageController : ControllerBase
    {
        private readonly ILogger<VantageController> _logger;
        private readonly AlphaVantageAPIService _vantageService;

        public VantageController(
            ILogger<VantageController> logger,
            AlphaVantageAPIService vantageService
            )
        {
            _logger = logger;
            _vantageService = vantageService;
        }

        [HttpGet()]
        [Route("{symbol}/Daily")]
        public async Task<JsonResult> Get([FromRoute] string symbol)
        {
            VantageDailyDTO returnValue = new VantageDailyDTO();

            Dictionary<string, JsonElement> result = await _vantageService
                .QueryAlphaVantage(VantageVerbs.daily,
                    new StockFilterDTO() {
                        Symbol = symbol
                    }
                );

            if (result.TryGetValue("Meta Data", out JsonElement metaData))
            {
                returnValue.Ticker = metaData.GetProperty("2. Symbol").GetString();
                returnValue.AsOfDate = metaData.GetProperty("3. Last Refreshed").GetDateTime();
            }

            if (result.TryGetValue("Time Series (Daily)", out JsonElement records))
            {
                List<JsonProperty> recentData = records.EnumerateObject().Take(2).ToList();

                Dictionary<string, string> recentData1= recentData.First().Value.Deserialize<Dictionary<string, string>>();
                returnValue.Price = Convert.ToDecimal(recentData1.GetValueOrDefault("4. close"));

                Dictionary<string, string> recentData2  = recentData.Last().Value.Deserialize<Dictionary<string, string>>();
                returnValue.PreviousClose = Convert.ToDecimal(recentData2.GetValueOrDefault("4. close"));
            }

            return new JsonResult(returnValue);
        }
    }
}