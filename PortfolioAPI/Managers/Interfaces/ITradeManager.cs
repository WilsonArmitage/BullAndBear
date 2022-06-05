using Common.Shared.DTO;

namespace PortfolioAPI.Managers.Interfaces
{
    public interface ITradeManager
    {
        Task<List<TradeDTO>> GetAll(TradeFilterDTO tradeFilter);

        Task<Guid> Save(TradeDTO trade);
    }
}