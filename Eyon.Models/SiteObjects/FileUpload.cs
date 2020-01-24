using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models.SiteObjects
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }
    }
}
