using System.ComponentModel.DataAnnotations;

namespace EngineReport.DTOs
{
    public class GetConsumerProcessReportDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }
    }
}
