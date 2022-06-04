namespace PortfolioAPI.SDK.Options
{
    public class PortfolioAPIOptions
    {
        public string BaseAddress { get; set; }

        public PortfolioAPIOptions(string baseAddress)
        {
            BaseAddress = baseAddress;
        }
    }
}