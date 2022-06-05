using PortfolioAPI.Models;

namespace PortfolioAPI.Repository.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetAll();
        Task<List<Portfolio>> Get(Guid portfolioId);

        Task<Guid> Save(Portfolio portfolio);

        Task<int> Delete(Guid portfolioId);

    }
}