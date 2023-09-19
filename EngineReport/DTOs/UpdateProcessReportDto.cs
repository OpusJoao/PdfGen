using System.ComponentModel.DataAnnotations;

namespace EngineReport.DTOs
{
    public class UpdateProcessReportDto
    {
        public Nullable<int> Status { get; set; }

        public string Link { get; set; } = string.Empty;

        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> FinalDate { get; set; }

        public Nullable<DateTime> CreatedAt { get; set; } = DateTime.Now;

        public Nullable<DateTime> UpdatedAt { get; set; } = DateTime.Now;

        public Nullable<DateTime> DeletedAt { get; set; }
    }
}
