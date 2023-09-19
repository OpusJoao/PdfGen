using Singulare.DTOs;

namespace Singulare.Services.ProcessReportService
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
            
            if( startDate is not null &&  finalDate is not null )
            {
                processReportsFound = await _context.ProcessReports.Where(processReport => processReport.StartDate >= startDate && processReport.FinalDate <= finalDate).ToListAsync();
            }else
            {
                processReportsFound = await _context.ProcessReports.ToListAsync();
            }

            return processReportsFound;
        }

        public async Task<ProcessReports?> GetProcessReportById(int id)
        {
            var processReportsFound = await _context.ProcessReports.FindAsync(id);

            return processReportsFound;
        }

        public Task<bool> UpdateProcessReport(int id)
        {
            throw new NotImplementedException();
        }
    }
}
