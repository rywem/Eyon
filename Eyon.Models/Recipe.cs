using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Recipe
    {        
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }        
        public string Instructions { get; set; }
        public string Cooktime { get; set; }
        
        public Community Community { get; set; }
        public ICollection<RecipeSiteImages> RecipeSiteImages { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<RecipeCategories> RecipeCategories { get; set; }


    }
}
