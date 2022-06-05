using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Managers.Interfaces;
using PortfolioAPI.SDK.Interfaces;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase, ITradeService
    {
        private readonly ILogger<TradeController> _logger;
        private ITradeManager _tradeManager;

        public TradeController(
            ILogger<TradeController> logger,
            ITradeManager tradeManager
        )
        {
            _logger = logger;
            _tradeManager = tradeManager;
        }

        [HttpGet()]
        [Route("{portfolioId}/All")]
        public async Task<IEnumerable<TradeDTO>> GetAll(Guid portfolioId)
        {
            return await _tradeManager.GetAll(new TradeFilterDTO() { PortfolioId = portfolioId });
        }

        [HttpPost()]
        [Route("Save")]
        public async Task<Guid> Save([FromBody] TradeDTO trade)
        {
            return await _tradeManager.Save(trade);
        }

        [HttpPost()]
        [Route("{tradeId}/Delete")]
        public Task<int> Delete(Guid tradeId)
        {
            throw new NotImplementedException();
        }
    }
}