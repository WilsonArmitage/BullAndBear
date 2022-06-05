using Common.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioAPI.SDK.Interfaces
{
    public interface ITradeService
    {
        //Task<IEnumerable<TradeDTO>> GetAll(TradeFilterDTO tradeFilter);
        Task<IEnumerable<TradeDTO>> GetAll(Guid portfolioId);
        Task<Guid> Save(TradeDTO trade);
        Task<int> Delete(Guid tradeId);
    }
}