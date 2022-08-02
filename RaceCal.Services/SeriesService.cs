using RaceCal.Data;
using RaceCal.Models;
using RaceCal.Models.Series;

namespace RaceCal.Services
{
    public class SeriesService : ISeriesService
    {
        private Guid _userId;
        private readonly ApplicationDbContext _ctx;

        public SeriesService(ApplicationDbContext context)
        {
            //_userId = userId
            _ctx = context;
        }

        public bool CreateSeries(SeriesCreate model)
        {
            var entity =
                new Series()
                {
                    //SeriesId = model.SeriesId,
                    OwnerId = _userId,
                    Title = model.Title,
                    CreatedUtc = DateTimeOffset.Now
                };

            _ctx.Serieses.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<SeriesListItem> GetSeries()
        {
            var query = _ctx.Serieses
                .OrderBy(x => x.Title)
            //.Where(e => e.OwnerId == _userId)
            .Select(e => new SeriesListItem
            {
                SeriesId = e.SeriesId,
                Title = e.Title,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc
            });

            return query.ToArray();
        }

        public SeriesDetail GetSeriesById(int id)
        {
            //using (var _ctx = new ApplicationDbContext())
            {
                var entity =
                    _ctx
                        .Serieses
                        .SingleOrDefault
                        (e => e.SeriesId == id
                        //&& e.OwnerId == _userId
                        );
                return
                    new SeriesDetail
                    {
                        SeriesId = entity.SeriesId,
                        Title = entity.Title,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdatedSeries(SeriesEdit model)
        {
            //using(var _ctx = new ApplicationDbContext())
            {
                var entity =
                    _ctx.Serieses.SingleOrDefault(e => e.SeriesId == model.SeriesId
                    //&& e.OwnerId == _userId
                    );

                entity.Title = model.Title;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return _ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteSeries(int seriesId)
        {
            var entity =
            _ctx
                .Serieses
                .SingleOrDefault(e => e.SeriesId == seriesId
                //&& e.OwnerId == _userId
                );

            _ctx.Serieses.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<SeriesListItem> GetAllSeries()
        {
            var series = _ctx.Serieses
                //.Where(e => e.OwnerId == _userId)
                .Select(e =>
                    new SeriesListItem()
                    {
                        SeriesId = e.SeriesId,
                        Title = e.Title,
                        //OwnerId = e.OwnerId
                    }).ToList();
            return series;
        }



        public void SetUserId(Guid userid) => _userId = userid;

    }
}