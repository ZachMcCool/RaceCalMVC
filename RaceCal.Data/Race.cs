using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceCal.Data
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name = "Race Name (ex: Indianapolis 500)")]
        public string Title { get; set; }
        //[Display(Name = "Race Info")]
        //public string Content { get; set; }
        [Required]
        [Display(Name = "Race Date and Time")]
        public DateTime RaceTime { get; set; }

        [Display(Name = "Race Broadcast")]
        public string Broadcast { get; set; }

        [ForeignKey(nameof(Series))]
        [Display(Name = "Series")]
        public int SeriesId { get; set; }
        public virtual Series Series { get; set; }

        [ForeignKey(nameof(Track))]
        public int TrackId { get; set; }
        [Display(Name = "Track")]
        public virtual Track Track { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }



    }
}
