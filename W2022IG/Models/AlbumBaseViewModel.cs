using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class AlbumBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        [Required, StringLength(512)]
        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Primary Genre")]
        public string Genre { get; set; }

        // User name who looks after the album
        [Required, StringLength(200)]
        public string Coordinator { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Album summary")]
        public string Summary { get; set; }

        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}