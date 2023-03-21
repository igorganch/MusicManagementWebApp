using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        // For an individual, can be birth name, or a stage/performer name
        // For a duo/band/group/orchestra, will typically be a stage/performer name
        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        // For an individual, a birth name
        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        // For an individual, a birth date
        // For all others, can be the date the artist started working together
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        [Display(Name = "Artist Photo")]
        [Required, StringLength(512)]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Primary genre")]
        public string Genre { get; set; }

        // User name who looks after this artist
        [Required, StringLength(200)]
        public string Executive { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }

    }
}