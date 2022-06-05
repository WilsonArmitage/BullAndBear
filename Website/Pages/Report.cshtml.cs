using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioAPI.SDK.Services;

namespace Website.Pages
{
    public class ReportModel : PageModel
    {
        private readonly ILogger<ReportModel> _logger;
        private PortfolioAPIService _portfolioAPIService;
        private TradeAPIService _tradeAPIService;

        [BindProperty]
        public List<PortfolioDTO> Portfolios { get; set; }

        public ReportModel(
            ILogger<ReportModel> logger,
            PortfolioAPIService portfolioAPIService,
            TradeAPIService tradeAPIService
            )
        {
            _logger = logger;
            _portfolioAPIService = portfolioAPIService;
            _tradeAPIService = tradeAPIService;
        }

        public async Task OnGetAsync()
        {
            Portfolios = (await _portfolioAPIService.GetAll()).ToList();

        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _portfolioAPIService.Delete(id);

            return RedirectToPage();
        }

    }
}