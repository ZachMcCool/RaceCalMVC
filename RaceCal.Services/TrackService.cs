using RaceCal.Data;
using RaceCal.Models;
using RaceCal.Models.Track;

namespace RaceCal.Services
{
    public class TrackService : ITrackService
    {
        private Guid _userId;
        private readonly ApplicationDbContext _ctx;

        public TrackService(ApplicationDbContext context)
        {
            //_userId = userId;
            _ctx = context;
        }

        public bool CreateTrack(TrackCreate model)
        {
            var entity =
                new Track()
                {
                    //TrackId = _TrackId,
                    OwnerId = _userId,
                    TrackName = model.TrackName,
                    Location = model.Location,
                    CreatedUtc = DateTime.UtcNow
                };

            _ctx.Tracks.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<TrackListItem> GetTracks()
        {
            var query = _ctx.Tracks
                .OrderBy(x => x.TrackName)
            //.Where(e => e.OwnerId == _userId)
            .Select(e => new TrackListItem
            {
                TrackId = e.TrackId,
                TrackName = e.TrackName,
                Location = e.Location,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc
            });

            return query.ToArray();
        }

        public TrackDetail GetTrackById(int id)
        {
            {
                var entity =
                    _ctx
                        .Tracks
                        .SingleOrDefault(e => e.TrackId == id
                        //&& e.OwnerId == _userId
                        );
                return
                    new TrackDetail
                    {
                        TrackId = entity.TrackId,
                        TrackName = entity.TrackName,
                        Location = entity.Location,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdatedTrack(TrackEdit model)
        {
            //using(var _ctx = new ApplicationDbContext())
            {
                var entity =
                    _ctx.Tracks.SingleOrDefault(e => e.TrackId == model.TrackId
                    //&& e.OwnerId == _userId
                    );
                entity.TrackId = model.TrackId;
                entity.TrackName = model.TrackName;
                entity.Location = model.Location;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return _ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrack(int trackId)
        {
            var entity =
            _ctx
                .Tracks
                .SingleOrDefault(e => e.TrackId == trackId
                //&& e.OwnerId == _userId
                );

            _ctx.Tracks.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<TrackListItem> GetAllTracks()
        {
            var tracks = _ctx.Tracks
                //.Where(e => e.OwnerId == _userId)
                .Select(e =>
                    new TrackListItem()
                    {
                        TrackId = e.TrackId,
                        TrackName = e.TrackName,
                        //OwnerId = e.OwnerId
                    });
            return tracks;
        }





        public void SetUserId(Guid userid) => _userId = userid;

    }
}
