using System.ComponentModel.DataAnnotations;

namespace Eyon.Models
{
    public class Country
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
