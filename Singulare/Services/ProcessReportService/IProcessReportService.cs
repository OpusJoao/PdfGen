using Singulare.DTOs;

namespace Singulare.Services.ProcessReportService
{
    public interface IProcessReportService
    {
        Task<List<ProcessReports>> GetProcessReport(Nullable<DateTime> startDate = null, Nullable<DateTime> finalDate = null);

        Task<ProcessReports?> GetProcessReportById(int id);

        Task<bool> UpdateProcessReport(int id);

        Task<bool> DeleteProcessReport(int id);

        ProcessReports? CreateProcessReport(CreateProcessReportDto processReport);
    }
}
