using System.ComponentModel.DataAnnotations;

namespace RaceCal.Models.Series
{
    public class SeriesDetail
    {
        public int SeriesId { get; set; }
        //public Guid OwnerId { get; set; }
        [Display(Name = "Series Name")]
        public string Title { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
