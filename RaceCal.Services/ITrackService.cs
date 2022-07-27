using RaceCal.Models;
using RaceCal.Models.Track;

namespace RaceCal.Services
{
    public interface ITrackService
    {
        bool CreateTrack(TrackCreate model);
        bool DeleteTrack(int trackId);
        TrackDetail GetTrackById(int id);
        IEnumerable<TrackListItem> GetTracks();
        bool UpdatedTrack(TrackEdit model);
        void SetUserId(Guid userId);

    }
}