using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022IG.Controllers
{
    public class MediaController : Controller
    {
        Manager m = new Manager();

        // GET: Media
        public ActionResult Index()
        {
            return View("Index", "Home");
        }

        // GET: Media/Details/5
        [Route("clip/{id}")]
        public ActionResult Details(int? id)
        {
            var media = m.GetTrackMediaByID(id.GetValueOrDefault());

            if (media == null)
            {
                return HttpNotFound();
            }
            else
            {
                /// ERRROR
                    return File(media.Media, media.MediaContentType);
                
 
            }
        }

        [Route("media/{stringId}")]
        public ActionResult Details(string stringId = "")
        {
            var media = m.GetArtistMediaByID(stringId);

            if (media == null)
            {
                return HttpNotFound();
            }
            else
            {

                return File(media.Content, media.ContentType);
            }
        }

        [Route("media/{stringId}/download")]
        public ActionResult DetailsDownload(string stringId = "")
        {
            var media = m.GetArtistMediaByID(stringId);

            if (media == null)
            {
                return HttpNotFound();
            }
            else
            {

                string fileExt;
                RegistryKey key;
                object val;

    
                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + media.ContentType, false);
                val = (key == null) ? null : key.GetValue("Extension", null);
                fileExt = (val == null) ? string.Empty : val.ToString();

         
                var  ex = new System.Net.Mime.ContentDisposition{
                    FileName = $"img-{stringId}{fileExt}",
                    Inline = false
                };
             
                Response.AppendHeader("Content-Disposition", ex.ToString());
                return File(media.Content, media.ContentType);
            }
        }
    }
}
