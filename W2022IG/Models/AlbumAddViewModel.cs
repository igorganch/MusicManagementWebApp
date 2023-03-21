using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class AlbumAddViewModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        [Required, StringLength(512)]
        public string UrlAlbum { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? GenreId { get; set; }

        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }

    }
}