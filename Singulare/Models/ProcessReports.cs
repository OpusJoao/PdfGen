using System.ComponentModel.DataAnnotations;

namespace Singulare.Models
{
    public class ProcessReports
    {
        public int Id { get; set; }

        [Required]
        public int Status { get; set; }
        
        public string Link { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime FinalDate { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Nullable<DateTime> DeletedAt { get; set; }
    }
}
