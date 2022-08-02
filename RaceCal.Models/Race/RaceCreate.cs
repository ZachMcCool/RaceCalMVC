using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models
{
    public class RaceCreate
    {
        [Required]
        [Display(Name = "Race Name (ex: Indianapolis 500)")]
        public string Title { get; set; }
        //[Display(Name = "Race Info")]
        //public string Content { get; set; }
        [Required]
        [Display(Name = "Race Date and Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]

        public DateTime RaceTime { get; set; }
        [Display(Name = "Series")]
        public int SeriesId { get; set; }
        [Display(Name = "Track")]
        public int TrackId { get; set; }

        [Display(Name = "Race Broadcast")]
        public string Broadcast { get; set; }
    }
}
