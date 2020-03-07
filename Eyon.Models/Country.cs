using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Country: INamed, ITopicType
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }

        [NotMapped]
        public TopicType Topic => TopicType.Country;
    }
}
