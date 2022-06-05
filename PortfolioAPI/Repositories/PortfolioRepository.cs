using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;
using PortfolioAPI.Repository.Interfaces;

namespace PortfolioAPI.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private PortfolioDbContext _dbContext { get; set; }

        public PortfolioRepository(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Portfolio>> Get(Guid portfolioId)
        {
            return await _dbContext.Portfolios
                  .Where(p => p.Id == portfolioId)
                  .Take(1)
                  .ToListAsync();
        }

        public async Task<List<Portfolio>> GetAll()
        {
            return await _dbContext.Portfolios
                .ToListAsync();
        }

        public async Task<Guid> Save(Portfolio portfolio)
        {
            if (portfolio.Id == Guid.Empty)
            {
                _dbContext.Add(portfolio);
            }
            else
            {
                _dbContext.Update(portfolio);
            }
            await _dbContext.SaveChangesAsync();

            return portfolio.Id;
        }

        public async Task<int> Delete(Guid portfolioId)
        {
            int returnValue = 0;

            if (portfolioId != Guid.Empty)
            {
                List<Portfolio> result = await _dbContext.Portfolios
                  .Where(p => p.Id == portfolioId)
                  .Take(1)
                  .ToListAsync();

                if (result.Any())
                {
                    _dbContext.Portfolios.RemoveRange(result);
                    returnValue = await _dbContext.SaveChangesAsync();
                }
            }

            return returnValue;
        }
    }
}
