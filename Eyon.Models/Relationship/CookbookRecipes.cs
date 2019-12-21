using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class CookbookRecipes
    {        
        public long CookbookId { get; set; }
        [ForeignKey("CookbookId")]
        public Cookbook Cookbook { get; set; }
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }        
    }
}
