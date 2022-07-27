using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceCal.Models;
using RaceCal.Models.Track;
using RaceCal.Services;
using System.Security.Claims;

namespace RaceCal.WebMVC.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        //private readonly Guid _userId;


        public TrackController(ITrackService trackService)
        {
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
            _trackService.SetUserId(userId);
            return true;
        }
        //private readonly ApplicationDbContext _ctx;

        //public TrackController(ApplicationDbContext ctx)
        //{
        //    _ctx = ctx;
        //}
        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            var tracks = _trackService.GetTracks();
            return View(tracks.ToList());

        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrackCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid) return View(model);

            if (_trackService.CreateTrack(model))
            {
                TempData["SaveResult"] = "Your track was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Track could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var model = _trackService.GetTrackById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var detail = _trackService.GetTrackById(id);
            var model =
                new TrackEdit
                {
                    TrackId = detail.TrackId,
                    TrackName = detail.TrackName,
                    Location = detail.Location
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TrackEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TrackId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();

            if (_trackService.UpdatedTrack(model))
            {
                TempData["SaveResult"] = "Your Track was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Track could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var model = _trackService.GetTrackById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrack(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            _trackService.DeleteTrack(id);
            TempData["SaveResult"] = "Your Track was deleted";
            return RedirectToAction("Index");
        }
    }
}
