using System;
using System.Collections.Generic;

namespace PortfolioAPI.Models
{
    public partial class Portfolio
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
