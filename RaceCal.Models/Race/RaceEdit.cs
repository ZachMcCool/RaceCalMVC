using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models.Race
{
    public class RaceEdit
    {
        public int RaceId { get; set; }
        [Display(Name = "Race Name (ex: Indianapolis 500)")]
        public string Title { get; set; }
        [Display(Name = "Race Info")]
        //public string Content { get; set; }
        //[Display(Name = "Race Date and Time")]
        public DateTime RaceTime { get; set; }
        [Display(Name = "Series")]
        public int SeriesId { get; set; }
        //public string SeriesTitle { get; set; }
        public string SeriesTitle { get; set; }


        [Display(Name = "Track")]
        public int TrackId { get; set; }
        //public string TrackName { get; set; }
        public string TrackName { get; set; }



        [Display(Name = "Race Broadcast")]
        public string Broadcast { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
