using System.ComponentModel.DataAnnotations;

namespace Singulare.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Open { get; set; }

        public int High { get; set; }
        
        public int Low { get; set; }

        public int Close { get; set; }

        public int AdjClose { get; set; }
        
        public int Volume { get; set; }
    }
}
