using Common.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioAPI.SDK.Interfaces
{
    public interface IVantageService
    {
        Task<StockDTO> GetStock(string verb, StockFilterDTO filter);
    }
}