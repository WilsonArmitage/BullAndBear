using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioAPI.SDK.Services;

namespace Website.Pages
{
    public class PortfolioModel : PageModel
    {
        private readonly ILogger<PortfolioModel> _logger;
        private PortfolioAPIService _portfolioAPIService;

        [BindProperty]
        public List<PortfolioDTO> Portfolios { get; set; }

        public PortfolioModel(
            ILogger<PortfolioModel> logger,
            PortfolioAPIService portfolioAPIService
            )
        {
            _logger = logger;
            _portfolioAPIService = portfolioAPIService;
        }

        public async Task OnGetAsync()
        {
            Portfolios = (await _portfolioAPIService.GetAll()).ToList();

        }
    }
}