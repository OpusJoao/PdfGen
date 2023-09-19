using EngineReport.DTOs;
using Microsoft.Extensions.Logging;

namespace EngineReport.Services.ProcessReportService
{
    public class ProcessReportService : IProcessReportService
    {
        private readonly DataContext _context;
        public ProcessReportService(DataContext context)
        {
            _context = context;
        }

        public ProcessReports? CreateProcessReport(CreateProcessReportDto processReport)
        {
            var processReportToCreate = new ProcessReports
            {
                StartDate = processReport.StartDate,
                FinalDate = processReport.FinalDate,
                Status = 1,
            };
            _context.ProcessReports.Add(processReportToCreate);
            _context.SaveChanges();
            return processReportToCreate;
        }

        public Task<bool> DeleteProcessReport(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProcessReports>> GetProcessReport(Nullable<DateTime> startDate = null, Nullable<DateTime> finalDate = null)
        {
            List<ProcessReports> processReportsFound;

            if (startDate is not null && finalDate is not null)
            {
                processReportsFound = await _context.ProcessReports.Where(processReport => processReport.StartDate >= startDate && processReport.FinalDate <= finalDate).ToListAsync();
            }
            else
            {
                processReportsFound = await _context.ProcessReports.ToListAsync();
            }

            return processReportsFound;
        }

        public async Task<ProcessReports?> GetProcessReportById(int id)
        {
            return await _context.ProcessReports.FindAsync(id);
        }

        public async Task<bool> UpdateProcessReport(int id, UpdateProcessReportDto processReport)
        {
            var currentProcessReport = await _context.ProcessReports.FindAsync(id);
            
            if(currentProcessReport is null) {
                return false;
            }

            currentProcessReport.Status = processReport.Status is not null ? (int)processReport.Status : currentProcessReport.Status;
            currentProcessReport.Link = processReport.Link != "" ? processReport.Link : currentProcessReport.Link;
            currentProcessReport.StartDate = processReport.StartDate ?? currentProcessReport.StartDate;
            currentProcessReport.FinalDate = processReport.FinalDate ?? currentProcessReport.FinalDate;

            currentProcessReport.UpdatedAt = DateTime.Now;

            int linesAffected = _context.SaveChanges();

            return linesAffected > 0;

        }

        public async Task<ProcessReports?> UpdateProcessReportStatus(int id, int status) {
            var processReportsToUpdate = new UpdateProcessReportDto
            {
                Status = status
            };
            var updated = await UpdateProcessReport(id, processReportsToUpdate);

            if(updated)
            {
                return await _context.ProcessReports.FindAsync(id);
            }

            return null;
        }

        public async Task<ProcessReports?> UpdateProcessReportLink(int id, string link)
        {
            var processReportsToUpdate = new UpdateProcessReportDto
            {
                Link = link
            };
            var updated = await UpdateProcessReport(id, processReportsToUpdate);

            if (updated)
            {
                return await _context.ProcessReports.FindAsync(id);
            }

            return null;
        }
    }
}
