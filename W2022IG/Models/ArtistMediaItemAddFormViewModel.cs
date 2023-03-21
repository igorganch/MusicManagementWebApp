using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class ArtistMediaItemAddFormViewModel
    {
        public int ArtistId { get; set; }

        [Display(Name = "Artist information")]
        public string ArtistInfo { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Media Item")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }
    }
}