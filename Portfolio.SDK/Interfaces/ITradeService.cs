using Common.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface ITradeService
    {
        Task<TradeDTO> Get(Guid id);
        Task<IEnumerable<TradeDTO>> GetAll(TradeFilterDTO tradeFilter);
        Task<Guid> Save(TradeDTO trade);
        Task<int> Delete(Guid id);
    }
}