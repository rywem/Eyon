using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models
{
    public class SiteImage
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string FileType { get; set; }
        public string Encoded { get; set; }
    }
}
