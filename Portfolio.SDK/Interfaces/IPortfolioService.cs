using Common.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioDTO> Get(Guid id);
        Task<IEnumerable<PortfolioDTO>> GetAll();
        Task<Guid> Save(PortfolioDTO portfolio);
        Task<int> Delete(Guid id);
    }
}