using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models
{
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
