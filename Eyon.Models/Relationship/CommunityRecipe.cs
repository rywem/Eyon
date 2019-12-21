using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class CommunityRecipe
    {
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public long CommunityId { get; set; }
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
    }
}
