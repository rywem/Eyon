using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Eyon.Models.ViewModels
{
    public class RecipeViewModel
    {
        public Recipe Recipe { get; set; }
        
        [Required]
        [DisplayName("Ingredients")]
        [MaxLength(5000)]
        public string IngredientsText { get; set; }
        [Required]
        [DisplayName("Instructions")]
        [MaxLength(5000)]
        public string InstructionsText { get;set; }
        public List<Category> Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }
        public Community Community { get; set; }
        public List<SiteImage> RecipeSiteImages { get; set; }
        public List<Cookbook> Cookbooks { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public RecipeViewModel()
        {
            this.Recipe = new Recipe();
            //this.Community = new Community();
            //this.Categories = new List<Category>();
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
            //this.RecipeSiteImages = new List<SiteImage>();
            //this.Cookbooks = new List<Cookbook>();
            //this.ApplicationUser = new ApplicationUser();
        }

    }
}
