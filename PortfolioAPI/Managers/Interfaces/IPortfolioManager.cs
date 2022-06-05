using Common.Shared.DTO;

namespace PortfolioAPI.Managers.Interfaces
{
    public interface IPortfolioManager
    {
        Task<List<PortfolioDTO>> GetAll(TradeFilterDTO tradeFilter);

        Task<Guid> Save(PortfolioDTO portfolio);
    }
}