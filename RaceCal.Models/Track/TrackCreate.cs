using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models
{
    public class TrackCreate
    {
        [Required]

        [Display(Name = "Track Name")]
        public string TrackName { get; set; }
        public string Location { get; set; }
    }
}
