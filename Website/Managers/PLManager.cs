using Common.Shared.DTO;
using PortfolioAPI.SDK.DTO;
using PortfolioAPI.SDK.Enumerations;
using PortfolioAPI.SDK.Services;
using Website.Managers.Intefaces;
using Website.Models;

namespace Website.Managers
{
    public class PLManager : IPLManager
    {
        private PortfolioAPIService _portfolioAPIService;
        private TradeAPIService _tradeAPIService;
        private VantageAPIService _vantageAPIService;

        public PLManager(
            PortfolioAPIService portfolioAPIService,
            TradeAPIService tradeAPIService,
            VantageAPIService vantageAPIService
            )
        {
            _portfolioAPIService = portfolioAPIService;
            _tradeAPIService = tradeAPIService;
            _vantageAPIService = vantageAPIService;
        }

        public async Task<PLReportModel> GetPLReport(Guid portfolioId)
        {
            PLReportModel reportModel = new PLReportModel();

            if (portfolioId != Guid.Empty)
            {
                reportModel.Portfolio = await _portfolioAPIService.Get(portfolioId);

                reportModel.Trades = (await _tradeAPIService.GetAll(portfolioId)).ToList();

                reportModel.TradeReports = reportModel.Trades
                    .GroupBy(x => x.Ticker)
                    .Select(x => new PLReportTradeModel()
                    {
                        Ticker = x.Key,
                        Quantity = x.Sum(t => t.TradeType == TradeType.Buy ? t.Quantity : -1 * t.Quantity),
                        Cost = x.Sum(t => t.TradeType == TradeType.Buy ? t.Quantity * t.Price : -1 * (t.Quantity * t.Price)),
                    })
                    .ToList();

                reportModel.TradeReports
                    .Select(async x => await FillVantageData(x))
                    .Select(t => t.Result)
                    .ToList();
            }

            return reportModel;
        }

        private async Task<PLReportTradeModel> FillVantageData(PLReportTradeModel model)
        {
            VantageDailyDTO result = await _vantageAPIService.GetDaily(model.Ticker);

            model.AsOfDate = result.AsOfDate;
            model.Price = result.Price;
            model.PreviousClose = result.PreviousClose;

            return model;
        }
    }
}
