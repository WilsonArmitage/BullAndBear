using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioAPI.SDK.Services;
using Website.Managers.Intefaces;
using Website.Models;

namespace Website.Pages
{
    public class ReportModel : PageModel
    {
        private readonly ILogger<ReportModel> _logger;
        private IPLManager _plManager;

        public PLReportModel Report { get; set; }

        public ReportModel(
            ILogger<ReportModel> logger,
            IPLManager plManager
            )
        {
            _logger = logger;
            _plManager = plManager;
        }

        public async Task OnGetAsync(Guid id)
        {
            Report = await _plManager.GetPLReport(id);
        }
    }
}