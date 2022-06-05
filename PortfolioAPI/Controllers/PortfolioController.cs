using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Managers.Interfaces;
using PortfolioAPI.SDK.Interfaces;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase, IPortfolioService
    {
        private readonly ILogger<PortfolioController> _logger;
        private IPortfolioManager _portfolioManager;

        public PortfolioController(
            ILogger<PortfolioController> logger,
            IPortfolioManager portfolioManager
        )
        {
            _logger = logger;
            _portfolioManager = portfolioManager;
        }

        [HttpGet()]
        [Route("{portfolioId}")]
        public async Task<PortfolioDTO> Get(Guid portfolioId)
        {
            List<PortfolioDTO> result = await _portfolioManager.GetAll(new TradeFilterDTO() { PortfolioId = portfolioId });

            return result
                .DefaultIfEmpty(new PortfolioDTO())
                .FirstOrDefault();
        }

        [HttpGet()]
        [Route("All")]
        public async Task<IEnumerable<PortfolioDTO>> GetAll()
        {
            return await _portfolioManager.GetAll(new TradeFilterDTO());
        }

        [HttpPost()]
        [Route("Save")]
        public async Task<Guid> Save(PortfolioDTO portfolio)
        {
            return await _portfolioManager.Save(portfolio);
        }

        [HttpPost()]
        [Route("{id}/Delete")]
        public async Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}