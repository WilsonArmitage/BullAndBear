using Common.Shared.DTO;

namespace Website.Models
{
    public class PLReportModel
    {
        public PLReportModel()
        {

        }

        public PortfolioDTO Portfolio { get; set; }

        public List<TradeDTO> Trades {get; set;}

        public List<PLReportTradeModel> TradeReports {get; set;}
    }
}
