using W2022IG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022IG.Controllers
{
    [Authorize()]
    public class AlbumController : Controller
    {
        private Manager m = new Manager();

        // GET: Album
        public ActionResult Index()
        {
            return View(m.GetAllAlbums());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var album = m.GetAlbumByID(id.GetValueOrDefault());

            if(album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        [Authorize(Roles = "Admin,Clerk")]
        [Route("Album/{id}/AddTrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.GetAlbumByID(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }

            var addform = new TrackAddFormViewModel();
            addform.GenreList = new SelectList(m.GetAllGenre(), "Id", "Name");
            addform.AlbumName = album.Name;
            addform.AlbumId = album.Id;

            return View(addform);
        }

        [Authorize(Roles = "Admin,Clerk")]
        [Route("Album/{id}/AddTrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel newTrack)
        {
            if (!ModelState.IsValid)
            {
                return View(newTrack);
            }

            var addedTrack = m.AddTrack(newTrack);

            if (addedTrack == null)
            {
                return View(newTrack);
            }
            else
            {
                return RedirectToAction("Details", "Track", new { id = addedTrack.Id });
            }
        }
    }
}
