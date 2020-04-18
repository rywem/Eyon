using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models.SiteObjects;
using Eyon.Models.Interfaces;

namespace Eyon.Models.ViewModels
{
    public class RecipeViewModel : IFeedItemViewModel
    {
        public Recipe Recipe { get; set; }        
        public bool IsOwner { get; set; }
        [Required]
        [DisplayName("Ingredients")]
        [MaxLength(5000)]
        public string IngredientText { get; set; }
        [Required]
        [DisplayName("Instructions")]
        [MaxLength(5000)]
        public string InstructionText { get;set; }

        public long CommunityId { get; set; }
        public string CommunityName { get; set; }
        public List<Ingredient> Ingredient { get; set; }
        public List<Instruction> Instruction { get; set; }
        public Community Community { get; set; }
        public List<UserImage> UserImage { get; set; }
        public ListItemSelector<Cookbook> CookbookSelector { get; set; }
        public ListItemSelector<Category> CategorySelector { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public RecipeViewModel()
        {
            this.IsOwner = false;
            this.Recipe = new Recipe();            
            this.CookbookSelector = new ListItemSelector<Cookbook>("Cookbook");
            this.CategorySelector = new ListItemSelector<Category>("Category");
            this.Ingredient = new List<Ingredient>();
            this.Instruction = new List<Instruction>();
        }

        public FeedItemViewModel ToFeedItemViewModel(Feed feed = null)
        {
            FeedItemViewModel feedItemViewModel = new FeedItemViewModel();
            if ( Community != null )
                feedItemViewModel.Communities.Add(Community);
            if ( CategorySelector.Items != null && CategorySelector.Items.Count > 0 )
                feedItemViewModel.Categories.AddRange(CategorySelector.Items);
            if ( CookbookSelector.Items != null && CookbookSelector.Items.Count > 0 )
                feedItemViewModel.Cookbooks.AddRange(CookbookSelector.Items);

            feedItemViewModel.Recipes.Add(this.Recipe);
            feedItemViewModel.FeedItem = this.Recipe;
            feedItemViewModel.UserImages = UserImage;

            if ( feed != null )
                feedItemViewModel.Feed = feed;
            return feedItemViewModel;
        }

        public List<Ingredient> ParseIngredients()
        {
            string[] ingredientsSplit = this.IngredientText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Ingredient = new List<Ingredient>();
            int step = 1;
            foreach ( var item in ingredientsSplit )
            {
                Ingredient.Add(new Ingredient()
                {
                    Text = item.Trim(),
                    Count = step,
                    RecipeId = Recipe.Id
                });
                step++;
            }
            return Ingredient;
        }

        public List<Instruction> ParseInstructions()
        {
            string[] instructionsSplit = InstructionText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int step = 1;
            Instruction = new List<Instruction>();
            foreach ( var item in instructionsSplit )
            {
                Instruction.Add(new Instruction()
                {
                    Count = step,
                    Text = item.Trim(),
                    RecipeId = Recipe.Id
                });
                step++;
            }
            return Instruction;
        }
    }
}
