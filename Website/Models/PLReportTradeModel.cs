namespace Website.Models
{
    public class PLReportTradeModel
    {
        public string Ticker { get; set; }
        public DateTime AsOfDate { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal MarketValue
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
        public decimal PreviousClose { get; set; }
        public decimal DailyPL
        {
            get
            {
                return this.Quantity * (this.Price - this.PreviousClose)
                    ;
            }
        }
        public decimal InceptionPL
        {
            get
            {
                return this.MarketValue - this.Cost;
            }
        }
    }
}
