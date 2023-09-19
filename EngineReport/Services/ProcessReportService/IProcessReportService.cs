using EngineReport.DTOs;

namespace EngineReport.Services.ProcessReportService
{
    public interface IProcessReportService
    {
        Task<List<ProcessReports>> GetProcessReport(Nullable<DateTime> startDate = null, Nullable<DateTime> finalDate = null);

        Task<ProcessReports?> GetProcessReportById(int id);

        Task<bool> UpdateProcessReport(int id, UpdateProcessReportDto processReport);

        Task<bool> DeleteProcessReport(int id);

        ProcessReports? CreateProcessReport(CreateProcessReportDto processReport);
        Task<ProcessReports?> UpdateProcessReportStatus(int id, int status);

        Task<ProcessReports?> UpdateProcessReportLink(int id, string link);
    }
}
