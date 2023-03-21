using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class TrackEditFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Track Clip")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }
    }
}