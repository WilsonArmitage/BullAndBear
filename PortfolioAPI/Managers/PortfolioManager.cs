using Common.Shared.DTO;
using PortfolioAPI.Managers.Interfaces;
using PortfolioAPI.Models;
using PortfolioAPI.Repository.Interfaces;

namespace PortfolioAPI.Managers
{
    public class PortfolioManager : IPortfolioManager
    {
        private IPortfolioRepository _portfolioRepository;
        private ITradeRepository _tradeRepository;

        public PortfolioManager(
            IPortfolioRepository portfolioRepository,
            ITradeRepository tradeRepository    
        )
        {
            _portfolioRepository = portfolioRepository;
            _tradeRepository = tradeRepository;
        }

        public async Task<List<PortfolioDTO>> GetAll(TradeFilterDTO tradeFilter)
        {
            List<Portfolio> response = new List<Portfolio>();

            if (tradeFilter.PortfolioId != Guid.Empty)
            {
                response.AddRange(await _portfolioRepository.Get(tradeFilter.PortfolioId));
            }
            else
            {
                response.AddRange(await _portfolioRepository.GetAll());
            }

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

        public async Task<bool> Delete(Guid portfolioId)
        {
            bool returrnValue = false;

            Portfolio portfolioEntity = new Portfolio();

            if (portfolioId != Guid.Empty)
            {
                if (await _portfolioRepository.Delete(portfolioId) > 0)
                {
                    returrnValue = true;
                    await _tradeRepository.Delete(portfolioId);
                }
            }

            return returrnValue;
        }
    }
}
