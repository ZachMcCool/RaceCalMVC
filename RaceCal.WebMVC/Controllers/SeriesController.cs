using Microsoft.AspNetCore.Mvc;
using RaceCal.Models;
using RaceCal.Models.Series;
using RaceCal.Services;
using System.Security.Claims;

namespace RaceCal.WebMVC.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesService _seriesService;
        private readonly Guid _userId;


        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService;
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
            _seriesService.SetUserId(userId);
            return true;
        }

        // GET: SeriesController
        public ActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            var series = _seriesService.GetSeries();
            return View(series.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(SeriesCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid) return View(model);

            if (_seriesService.CreateSeries(model))
            {
                TempData["SaveResult"] = "Your Series was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Series could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _seriesService.GetSeriesById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var detail = _seriesService.GetSeriesById(id);
            var model =
                new SeriesEdit
                {
                    SeriesId = detail.SeriesId,
                    Title = detail.Title,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SeriesEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SeriesId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();

            if (_seriesService.UpdatedSeries(model))
            {
                TempData["SaveResult"] = "Your series was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your series could not be updated.");
            return View();
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var model = _seriesService.GetSeriesById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSeries(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            _seriesService.DeleteSeries(id);
            TempData["SaveResult"] = "Your Series was deleted";
            return RedirectToAction("Index");

        }
    }

}
