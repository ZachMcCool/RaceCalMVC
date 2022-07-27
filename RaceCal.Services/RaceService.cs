using Microsoft.EntityFrameworkCore;
using RaceCal.Data;
using RaceCal.Models;
using RaceCal.Models.Race;


namespace RaceCal.Services
{
    public class RaceService : IRaceService
    {
        private Guid _userId;
        private readonly ApplicationDbContext _ctx;

        public RaceService(ApplicationDbContext context)
        {
            //_userId = userId;
            _ctx = context;
        }

        public bool CreateRace(RaceCreate model)
        {
            var raceEntity =
                new Race
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    //Content = model.Content,
                    RaceTime = model.RaceTime,
                    Broadcast = model.Broadcast,
                    SeriesId = model.SeriesId,
                    TrackId = model.TrackId,
                    CreatedUtc = DateTimeOffset.Now

                };

            _ctx.Races.Add(raceEntity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<RaceListItem> GetRaces()
        {
            var races = _ctx.Races
            //.Where(e => e.OwnerId == _userId)
            .Include("Series")
            .Include("Track")
            .OrderBy(r => r.RaceTime)
            .Select(e => new RaceListItem()
            {
                //RaceId = e.RaceId,
                RaceId = e.RaceId,
                Title = e.Title,
                //Content = e.Content,
                RaceTime = e.RaceTime,
                Broadcast = e.Broadcast,
                SeriesTitle = e.Series.Title,
                TrackName = e.Track.TrackName,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc
            }).ToList();

            return races;
        }

        public RaceDetail GetRaceById(int id)
        {
            //using (var _ctx = new ApplicationDbContext())
            {
                var race = _ctx.Races
                    .Single(e => e.RaceId == id
                    //&& e.OwnerId == _userId
                    );
                return
                    new RaceDetail
                    {
                        RaceId = race.RaceId,
                        Title = race.Title,
                        //Content = race.Content,
                        RaceTime = race.RaceTime,

                        SeriesId = race.SeriesId,
                        TrackId = race.TrackId,

                        Broadcast = race.Broadcast,
                        CreatedUtc = race.CreatedUtc,
                        ModifiedUtc = race.ModifiedUtc
                    };
            }
        }

        public bool UpdatedRace(RaceEdit model)
        {
            //using(var _ctx = new ApplicationDbContext())
            {
                var race = _ctx.Races
                    .Include("Series")
                    .Include("Track")
                    .Single(e => e.RaceId == model.RaceId
                    //&& e.OwnerId == _userId
                    );

                race.Title = model.Title;
                //race.Content = model.Content;
                race.RaceTime = model.RaceTime;
                //race.Content = model.Content;
                race.Broadcast = model.Broadcast;
                race.SeriesId = model.SeriesId;
                race.TrackId = model.TrackId;
                race.ModifiedUtc = DateTimeOffset.Now;

                return _ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRace(int raceId)
        {
            var entity = _ctx.Races
                .SingleOrDefault(e => e.RaceId == raceId
                //&& e.OwnerId == _userId
                );

            _ctx.Races.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<SeriesListItem> CreateSeriesDropDownList()
        {
            var seriesService = new SeriesService(_ctx);
            seriesService.SetUserId(_userId);
            var userSeries = seriesService.GetAllSeries();
            //Try Deleting this after it works 
            //.Where(e => e.OwnerId == _userId);
            return userSeries;
        }

        public IEnumerable<TrackListItem> CreateTrackDropDownList()
        {
            var trackService = new TrackService(_ctx);
            trackService.SetUserId(_userId);
            var userTracks = trackService.GetAllTracks();
            //Try Deleting this after it works 
            //.Where(e => e.OwnerId == _userId);
            return userTracks;
        }



        public void SetUserId(Guid userid) => _userId = userid;
    }




}
