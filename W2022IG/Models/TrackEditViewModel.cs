using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class TrackEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }
    }
}