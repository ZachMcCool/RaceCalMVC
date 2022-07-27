using System.ComponentModel.DataAnnotations;

namespace RaceCal.Data
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        public Guid OwnerId { get; set; }

        //[ForeignKey(nameof(TrackName))]
        //public int TrackNameId { get; set; }

        [Required]
        [Display(Name = "Track Name")]
        //I want to show this as a dropdown menu in my Race model
        public string TrackName { get; set; }

        public string Location { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

    }


}
