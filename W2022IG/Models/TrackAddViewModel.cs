using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class TrackAddViewModel
    {
        [Required, StringLength(200)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? GenreId { get; set; }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

        public string AlbumName { get; set; }

        public int AlbumId { get; set; }

        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }
    }
}