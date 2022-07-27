using RaceCal.Models;
using RaceCal.Models.Series;

namespace RaceCal.Services
{
    public interface ISeriesService
    {
        bool CreateSeries(SeriesCreate model);
        bool DeleteSeries(int seriesId);
        IEnumerable<SeriesListItem> GetSeries();
        SeriesDetail GetSeriesById(int id);
        bool UpdatedSeries(SeriesEdit model);
        void SetUserId(Guid userId);

    }
}