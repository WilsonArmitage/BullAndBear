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

        public async Task<List<Trade>> Get(Guid tradeId)
        {
            return await _dbContext.Trades
                  .Where(p => p.Id == tradeId)
                  .Take(1)
                  .ToListAsync();
        }

        public async Task<List<Trade>> GetAll(Guid portfolioId)
        {
            return await _dbContext.Trades
                .Where(t => t.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Guid> Save(Trade trade)
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

        public async Task<int> Delete(Guid portfolioId)
        {
            int returnValue = 0;

            if (portfolioId != Guid.Empty)
            {
                List<Trade> result = await _dbContext.Trades
                  .Where(t => t.PortfolioId == portfolioId)
                  .ToListAsync();

                if (result.Any())
                {
                    _dbContext.Trades.RemoveRange(result);
                    returnValue = await _dbContext.SaveChangesAsync();
                }
            }
            
            return returnValue;
        }
    }
}
