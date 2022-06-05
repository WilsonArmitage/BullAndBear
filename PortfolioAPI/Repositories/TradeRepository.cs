using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;
using PortfolioAPI.Repository.Interfaces;

namespace PortfolioAPI.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private PortfolioDbContext _dbContext { get; set; }

        public TradeRepository(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<List<Trade>> ITradeRepository.Get(Guid tradeId)
        {
            return await _dbContext.Trades
                  .Where(p => p.Id == tradeId)
                  .Take(1)
                  .ToListAsync();
        }

        async Task<List<Trade>> ITradeRepository.GetAll(Guid portfolioId)
        {
            return await _dbContext.Trades
                .Where(t => t.PortfolioId == portfolioId)
                .ToListAsync();
        }

        async Task<Guid> ITradeRepository.Save(Trade trade)
        {
            if (trade.Id == Guid.Empty)
            {
                _dbContext.Add(trade);
            }
            else
            {
                _dbContext.Update(trade);
            }
            await _dbContext.SaveChangesAsync();

            return trade.Id;
        }
    }
}
