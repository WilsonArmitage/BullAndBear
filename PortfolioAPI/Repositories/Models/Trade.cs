using System;
using System.Collections.Generic;

namespace PortfolioAPI.Models
{
    public partial class Trade
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string Ticker { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
        public bool Buy { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
