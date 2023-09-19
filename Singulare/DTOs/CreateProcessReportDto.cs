using System.ComponentModel.DataAnnotations;

namespace Singulare.DTOs
{
    public class CreateProcessReportDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }
    }
}
