using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? GenreId { get; set; }

        // User name who added/edited the track
        [Required, StringLength(200)]
        public string Clerk { get; set; }

        [Display(Name = "Track Clip")]
        public string MediaUrl
        {
            get
            {
                return $"/clip/{Id}";
            }
        }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

        //[Display(Name = "Album name")]
        //public IEnumerable<string> AlbumNames { get; set; }
    }
}