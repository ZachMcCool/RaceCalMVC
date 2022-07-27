using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models.Series
{
    public class SeriesEdit
    {
        public int SeriesId { get; set; }

        [Display(Name = "Series Name")]
        public string Title { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
