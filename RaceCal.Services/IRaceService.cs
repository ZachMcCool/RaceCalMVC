using RaceCal.Models;
using RaceCal.Models.Race;

namespace RaceCal.Services
{
    public interface IRaceService
    {
        bool CreateRace(RaceCreate model);
        bool DeleteRace(int raceId);
        RaceDetail GetRaceById(int id);
        IEnumerable<RaceListItem> GetRaces();
        bool UpdatedRace(RaceEdit model);

        void SetUserId(Guid userId);
        IEnumerable<SeriesListItem> CreateSeriesDropDownList();
        IEnumerable<TrackListItem> CreateTrackDropDownList();
    }
}