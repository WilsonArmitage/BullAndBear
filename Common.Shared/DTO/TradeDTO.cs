namespace Common.Shared.DTO
{
    public class TradeDTO
    {
        public Guid PortfolioId { get; set; }
        public Guid TradeId { get; set; }
        public string Ticker { get; set; }
        public DateTime TradeDate { get; set; }
        public TradeType TradeType { get; set; }
        public int Quantity {get; set;} 
        public decimal Price {get; set;}
    }
}