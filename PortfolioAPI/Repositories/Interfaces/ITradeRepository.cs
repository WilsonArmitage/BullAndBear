using PortfolioAPI.Models;

namespace PortfolioAPI.Repository.Interfaces
{
    public interface ITradeRepository
    {
        public Task<List<Trade>> GetAll(Guid portfolioId);
        public Task<List<Trade>> Get(Guid tradeId);

        public Task<Guid> Save(Trade trade);
        public Task<int> Delete(Guid portfolioId);
    }
}