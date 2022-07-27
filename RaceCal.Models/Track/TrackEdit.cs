using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models.Track
{
    public class TrackEdit
    {
        public int TrackId { get; set; }

        [Display(Name = "Track Name")]
        public string TrackName { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
