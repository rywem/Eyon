using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class RecipeSiteImage
    {        
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public long SiteImageId { get; set; }
        [ForeignKey("SiteImageId")]
        public SiteImage SiteImage { get; set; }        
    }
}
