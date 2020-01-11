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
        public bool IsOwner { get; set; }
        [Required]
        [DisplayName("Ingredients")]
        [MaxLength(5000)]
        public string IngredientsText { get; set; }
        [Required]
        [DisplayName("Instructions")]
        [MaxLength(5000)]
        public string InstructionsText { get;set; }

        public long CommunityId { get; set; }
        public string CommunityName { get; set; }
        public List<Category> Category { get; set; }
        public List<Ingredient> Ingredient { get; set; }
        public List<Instruction> Instruction { get; set; }
        public Community Community { get; set; }
        public List<UserImage> UserImage { get; set; }
        public List<Cookbook> Cookbooks { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public string CookbookIds { get; set; }
        
        public RecipeViewModel()
        {
            this.IsOwner = false;
            this.Recipe = new Recipe();
            //this.Community = new Community();
            //this.Categories = new List<Category>();
            this.Ingredient = new List<Ingredient>();
            this.Instruction = new List<Instruction>();
            //this.RecipeSiteImages = new List<SiteImage>();
            //this.Cookbooks = new List<Cookbook>();
            //this.ApplicationUser = new ApplicationUser();
        }

    }
}
