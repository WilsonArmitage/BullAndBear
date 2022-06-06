namespace PortfolioAPI.SDK.DTO
{
    public class VantageDailyDTO
    {
        public string Ticker { get; set; }
        public DateTime AsOfDate { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousClose { get; set; }
    }
}