using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Instruction : Interfaces.ICount
    {
        [Key]
        public long Id { get; set; }        
        [Required]
        public int Count { get; set; }
        [Required]
        [MaxLength(3000)]
        [StringLength(3000)]
        public string Text { get; set; }
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
    }
}
