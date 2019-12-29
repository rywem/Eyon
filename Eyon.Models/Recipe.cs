using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Recipe : IRecord
    {        
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }                
        [Required]
        public string Cooktime { get; set; }
        [Required]
        public string PrepTime { get; set; }
        [Required]
        public string Servings { get; set; }        
        [MaxLength(5000)]
        [Required]
        public string Description { get; set; } 
        
        public CommunityRecipe CommunityRecipe { get; set; }
        public ICollection<Instruction> Instructions { get; set; }
        public ICollection<RecipeSiteImage> RecipeSiteImages { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<RecipeCategory> RecipeCategories { get; set; }
        public ICollection<CookbookRecipe> CookbookRecipes { get; set; }
        public ApplicationUserRecipe ApplicationUserRecipe { get; set; }


        // TODO: Add EAV table with:  Calories, Yield, ReadyIn


    }
}
