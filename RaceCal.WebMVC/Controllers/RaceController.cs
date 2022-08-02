using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaceCal.Models;
using RaceCal.Models.Race;
using RaceCal.Services;
using System.Security.Claims;


namespace RaceCal.WebMVC.Controllers
{
    [Authorize]
    public class RaceController : Controller
    {
        private readonly IRaceService _raceService;
        private readonly ISeriesService _seriesService;
        private readonly ITrackService _trackService;
        //private readonly Guid _userId;

        public RaceController(IRaceService raceService, ISeriesService seriesService, ITrackService trackService)
        {
            _raceService = raceService;
            _seriesService = seriesService;
            _trackService = trackService;
        }


        //In this method,we will first need to get the User Id as a string from the token -> data from the token is stored
        //in the User.Claims property. We can use the first() method to find the User Id within the claims object, like so:

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null) return default;
            return Guid.Parse(userIdClaim);
        }

        //Now we can use the GetUserId() to make the SetUserInService method -> this should return a bool that tells us
        //whether or not the User Id can successfuly be set in the service (that is, if the user is logged in with a valid
        //session
        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            //if everything works from above...
            _raceService.SetUserId(userId);
            return true;
        }

        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            var races = _raceService.GetRaces();
            return View(races.ToList());



        }


        //GET
        public ActionResult Create()
        {
            ViewBag.SeriesSelectList = new SelectList(GetSeriesDropDownList(), "SeriesId", "Title");
            ViewBag.TrackSelectList = new SelectList(GetTrackDropDownList(), "TrackId", "TrackName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RaceCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.SeriesSelectList = new SelectList(GetSeriesDropDownList(), "SeriesId", "Title");
            ViewBag.TrackSelectList = new SelectList(GetTrackDropDownList(), "TrackId", "TrackName");

            if (_raceService.CreateRace(model))
            {
                TempData["SaveResult"] = "Your race was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Race could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _raceService.GetRaceById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.SeriesSelectList = new SelectList(GetSeriesDropDownList(), "SeriesId", "Title");
            ViewBag.TrackSelectList = new SelectList(GetTrackDropDownList(), "TrackId", "TrackName");

            if (!SetUserIdInService()) return Unauthorized();


            var detail = _raceService.GetRaceById(id);
            var model =
                new RaceEdit
                {
                    RaceId = detail.RaceId,
                    Title = detail.Title,
                    RaceTime = detail.RaceTime,
                    SeriesId = detail.SeriesId,
                    TrackId = detail.TrackId,
                    Broadcast = detail.Broadcast,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RaceEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RaceId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();
            if (_raceService.UpdatedRace(model))
            {
                TempData["SaveResult"] = "Your race was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your race could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _raceService.GetRaceById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRace(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            _raceService.DeleteRace(id);

            TempData["SaveResult"] = "Your Race was deleted";
            return RedirectToAction("Index");
        }

        private IEnumerable<SeriesListItem> GetSeriesDropDownList()
        {
            if (!SetUserIdInService()) return default;
            return _raceService.CreateSeriesDropDownList();
        }

        private IEnumerable<TrackListItem> GetTrackDropDownList()
        {
            if (!SetUserIdInService()) return default;
            return _raceService.CreateTrackDropDownList();
        }
    }
}
