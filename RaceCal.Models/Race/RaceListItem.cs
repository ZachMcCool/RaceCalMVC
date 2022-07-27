using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceCal.Models
{
    public class RaceListItem
    {
        //[Key]
        public int RaceId { get; set; }
        [Required]
        [Display(Name = "Race Name")]
        public string Title { get; set; }
        //[Display(Name = "Race Info")]
        //public string Content { get; set; }
        [Required]
        [Display(Name = "Race Date and Time")]
        public DateTime RaceTime { get; set; }
        [Display(Name = "Race Series")]

        //[ForeignKey(nameof(Series))]
        //public int SeriesId { get; set; }
        //public virtual RaceCal.Data.Series Series { get; set; }
        public string SeriesTitle { get; set; }


        [Display(Name = "Race Track")]

        //[ForeignKey(nameof(Track))]
        //public int TrackId { get; set; }
        //public virtual RaceCal.Data.Track Track { get; set; }

        public string TrackName { get; set; }



        [Display(Name = "Race Broadcast")]
        public string Broadcast { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
