using Common.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface IVantageService
    {
        Task<StockDTO> GetStock(string verb, StockFilterDTO filter);
    }
}