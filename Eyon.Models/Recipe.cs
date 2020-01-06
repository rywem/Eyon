using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Recipe : IHasOwners<ApplicationUserRecipe>, ICreated
    {        
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [DisplayName("Recipe Name")]
        public string Name { get; set; }
        [DisplayName("Cooktime")]
        [MaxLength(100)]
        [StringLength(100)]
        public string Cooktime { get; set; }
        [DisplayName("Prep time")]
        [MaxLength(100)]
        [StringLength(100)]
        public string PrepTime { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        public string Servings { get; set; }
        public Privacy Privacy { get; set; }
        [MaxLength(3000)]
        [StringLength(3000)]
        [Required]
        public string Description { get; set; } 
        
        public DateTime CreationDateTime { get; set; } 
        public CommunityRecipe CommunityRecipe { get; set; }
        public ICollection<Instruction> Instructions { get; set; }
        public ICollection<RecipeUserImage> RecipeUserImages { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<RecipeCategory> RecipeCategories { get; set; }
        public ICollection<CookbookRecipe> CookbookRecipes { get; set; }        
        public ICollection<ApplicationUserRecipe> ApplicationUserOwners { get; set; }

        // TODO: Add EAV table with:  Calories, Yield, ReadyIn


    }
}
