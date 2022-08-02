namespace RaceCal.Models
{
    public class RaceDetail
    {
        public int RaceId { get; set; }
        public Guid OwnerId { get; set; }
        //[Display(Name = "Race Name (ex: Indianapolis 500)")]
        public string Title { get; set; }
        //[Display(Name = "Race Info")]
        //public string Content { get; set; }
        //[Display(Name = "Race Date and Time")]


        public DateTime RaceTime { get; set; }
        //[Display(Name = "Race Series")]
        public int SeriesId { get; set; }
        //[Display(Name = "Race Broadcast")]
        //virtual Series Series { get; set; }
        public int TrackId { get; set; }

        public string Broadcast { get; set; }
        //[Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        //[Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
