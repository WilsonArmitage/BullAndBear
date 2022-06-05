using Common.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioAPI.SDK.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioDTO> Get(Guid portfolioId);
        Task<IEnumerable<PortfolioDTO>> GetAll();
        Task<Guid> Save(PortfolioDTO portfolio);
        Task<int> Delete(Guid portfolioId);
    }
}