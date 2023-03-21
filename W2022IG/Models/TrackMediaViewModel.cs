using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W2022IG.Models
{
    public class TrackMediaViewModel
    {
        public int Id { get; set; }
        public string MediaContentType { get; set; }
        public byte[] Media { get; set; }
    }
}