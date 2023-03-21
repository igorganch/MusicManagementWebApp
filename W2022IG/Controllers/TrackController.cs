using W2022IG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022IG.Controllers
{
    [Authorize()]
    public class TrackController : Controller
    {
        private Manager m = new Manager();

        // GET: Track
        public ActionResult Index()
        {
            var tracks = m.GetAllTrack();
            return View(tracks);
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            var track = m.GetTrackByID(id.GetValueOrDefault());

            if(track == null)
            {
                return HttpNotFound();
            }

            return View(track);
        }

        // GET: Track/Edit/5
        [Authorize(Roles = "Admin,Clerk")]
        public ActionResult Edit(int? id)
        {
            var track = m.GetTrackByID(id.GetValueOrDefault());

            if(track == null)
            {
                return HttpNotFound();
            }

            var form = m.mapper.Map<TrackEditFormViewModel>(track);

            return View(form);
        }
        // POST: Track/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Clerk")]
        public ActionResult Edit(int? id, TrackEditViewModel track)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = track.Id });
            }

            if (id.GetValueOrDefault() != track.Id)
            {
                return RedirectToAction("Index");
            }
            var editedPlaylist = m.EditTrack(track);

            if (editedPlaylist == null)
            {
                return RedirectToAction("Edit", new { id = track.Id });
            }
            else
            {
                return RedirectToAction("Details", new { id = track.Id });
            }
        }

        // GET: Track/Delete/5
        [Authorize(Roles = "Admin,Clerk")]
        public ActionResult Delete(int? id)
        {
            var track = m.GetTrackByID(id.GetValueOrDefault());

            if(track == null)
            {

                return RedirectToAction("Index");
            }

            return View(track);
        }

        // POST: Track/Delete/5
        [Authorize(Roles = "Admin,Clerk")]
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            m.DeleteTrack(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }
    }
}
