using Common.Shared.DTO;
using PortfolioAPI.Managers.Interfaces;
using PortfolioAPI.Models;
using PortfolioAPI.Repository.Interfaces;

namespace PortfolioAPI.Managers
{
    public class PortfolioManager : IPortfolioManager
    {
        private IPortfolioRepository _portfolioRepository;

        public PortfolioManager(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<List<PortfolioDTO>> GetAll(TradeFilterDTO tradeFilter)
        {
            List<Portfolio> response = await _portfolioRepository.GetAll();

            return response.Select(p =>
                new PortfolioDTO()
                {
                    PortfolioId = p.Id,
                    Name = p.Name
                })
                .ToList();
        }

        public async Task<Guid> Save(PortfolioDTO portfolio)
        {
            Portfolio portfolioEntity = new Portfolio();
            if (portfolio.PortfolioId != Guid.Empty)
            {
                List<Portfolio> existing = await _portfolioRepository.Get(portfolio.PortfolioId);
                if (existing.Any())
                {
                    portfolioEntity = existing.First();
                }
            }

            portfolioEntity.Name = portfolio.Name;

            return await _portfolioRepository.Save(portfolioEntity);
        }
    }
}
