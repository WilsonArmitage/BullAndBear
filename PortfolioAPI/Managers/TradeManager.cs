using Common.Shared.DTO;
using PortfolioAPI.Managers.Interfaces;
using PortfolioAPI.Models;
using PortfolioAPI.Repository.Interfaces;

namespace PortfolioAPI.Managers
{
    public class TradeManager : ITradeManager
    {
        private ITradeRepository _tradeRepository;

        public TradeManager(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<List<TradeDTO>> GetAll(TradeFilterDTO tradeFilter)
        {
            List<Trade> response = await _tradeRepository.GetAll(tradeFilter.PortfolioId);

            return response.Select(t =>
                new TradeDTO()
                {
                    TradeId = t.Id,
                    PortfolioId = t.PortfolioId,
                    Ticker = t.Ticker,
                    TradeDate = t.Date.DateTime,
                    TradeType = t.Buy ? TradeType.Buy : TradeType.Sell,
                    Price = t.Price,
                    Quantity = t.Quantity

                })
                .ToList();
        }

        public async Task<Guid> Save(TradeDTO trade)
        {
            Trade tradeEntity = new Trade();
            if(trade.TradeId != Guid.Empty)
            {
                List<Trade> existing = await _tradeRepository.Get(trade.TradeId);
                if(existing.Any())
                {
                    tradeEntity = existing.First();
                }
            }

            tradeEntity.PortfolioId = trade.PortfolioId;
            tradeEntity.Ticker = trade.Ticker.Truncate(5);
            tradeEntity.Date = DateTime.Now;
            tradeEntity.Buy = trade.TradeType == TradeType.Buy;
            tradeEntity.Quantity = trade.Quantity;
            tradeEntity.Price = trade.Price;

            return await _tradeRepository.Save(tradeEntity);
        }
    }

    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
