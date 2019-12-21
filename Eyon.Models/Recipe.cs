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
        public string Cooktime { get; set; }        
        public CommunityRecipe CommunityRecipe { get; set; }
        public ICollection<Instruction> Instructions { get; set; }
        public ICollection<RecipeSiteImage> RecipeSiteImages { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredient { get; set; }
        public ICollection<RecipeCategory> RecipeCategories { get; set; }
        public ICollection<CookbookRecipe> CookbookRecipes { get; set; }
        public ICollection<ApplicationUserRecipe> ApplicationUserRecipes { get; set; }


    }
}
