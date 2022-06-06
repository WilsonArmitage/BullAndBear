using Website.Models;

namespace Website.Managers.Intefaces
{
    public interface IPLManager
    {
        Task<PLReportModel> GetPLReport(Guid portfolioId);
    }
}