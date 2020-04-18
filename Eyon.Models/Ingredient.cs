using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Ingredient : Interfaces.ICount
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public int Count { get; set; }
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
    }
}
