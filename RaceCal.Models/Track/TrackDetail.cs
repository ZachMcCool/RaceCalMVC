using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models.Track
{
    public class TrackDetail
    {
        public int TrackId { get; set; }
        //public Guid OwnerId { get; set; }
        [Display(Name = "Track Name")]
        public string TrackName { get; set; }
        public string Location { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
