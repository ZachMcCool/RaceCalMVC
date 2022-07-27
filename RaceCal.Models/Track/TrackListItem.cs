namespace RaceCal.Models
{
    public class TrackListItem
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public Guid OwnerId { get; set; }
    }
}
