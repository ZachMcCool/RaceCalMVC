using System.ComponentModel.DataAnnotations;

namespace RaceCal.Data
{
    public class Series
    {
        [Key]
        public int SeriesId { get; set; }
        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "Series Name")]
        public string Title { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
