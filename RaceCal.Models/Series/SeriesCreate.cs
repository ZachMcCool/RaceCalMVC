using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models
{
    public class SeriesCreate
    {
        [Required]
        [Display(Name = "Series Name")]
        public string Title { get; set; }
    }
}
