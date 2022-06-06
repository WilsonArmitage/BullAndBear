using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioAPI.SDK.Services;

namespace Website.Pages
{
    public class ManagePortfolioModel : PageModel
    {
        private readonly ILogger<ManagePortfolioModel> _logger;
        private PortfolioAPIService _portfolioAPIService;
        private TradeAPIService _tradeAPIService;

        [BindProperty]
        public PortfolioDTO Portfolio { get; set; }

        [BindProperty]
        public List<TradeDTO> Trades { get; set; }

        [BindProperty]
        public TradeDTO Trade { get; set; }

        public ManagePortfolioModel(
            ILogger<ManagePortfolioModel> logger,
            PortfolioAPIService portfolioAPIService,
            TradeAPIService tradeAPIService
            )
        {
            _logger = logger;
            _portfolioAPIService = portfolioAPIService;
            _tradeAPIService = tradeAPIService;
        }

        public async Task OnGetAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                Portfolio = await _portfolioAPIService.Get(id);
            }

            if(Portfolio != null
                && Portfolio.PortfolioId != Guid.Empty)
            {
                Trades = (await _tradeAPIService.GetAll(id)).ToList();
            }
            else
            {
                Portfolio = new PortfolioDTO();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            if (!TryValidateModel(Portfolio, nameof(Portfolio)))
            {
                return Page();
            }

            if (Portfolio != null)
            {
                Guid returnValue = await _portfolioAPIService.Save(Portfolio);
            }

            return RedirectToPage("./Portfolio");
        }

        public async Task<IActionResult> OnPostTradeAsync(Guid id)
        {
            ModelState.Clear();
            if (!TryValidateModel(Trade, nameof(Trade)))
            {
                return Page();
            }

            if (Trade != null)
            {
                Trade.PortfolioId = id;
                await _tradeAPIService.Save(Trade);
            }

            return RedirectToPage("ManagePortfolio", new { id = id });
        }
    }
}