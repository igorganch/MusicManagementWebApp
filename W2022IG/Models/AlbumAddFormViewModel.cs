using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022IG.Models
{
    public class AlbumAddFormViewModel
    {
        public AlbumAddFormViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

       

        [Required, StringLength(512)]
        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }


        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? GenreId { get; set; }


        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }


        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }
    }
}