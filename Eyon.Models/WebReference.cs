using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class WebReference
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(2048)]
        [StringLength(2048)]        
        public string Url { get; set; }        
        public bool PreferSSL { get; set; } = true;
    }
}
