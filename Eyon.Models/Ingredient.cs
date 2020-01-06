using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public int Number { get; set; }
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
    }
}
