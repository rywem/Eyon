using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Category : ITopicItem
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }                
        public long SiteImageId { get; set; }
        [ForeignKey("SiteImageId")]
        public SiteImage SiteImage { get; set; }

        public ICollection<CookbookCategory> CookbookCategory { get; set; }
        public ICollection<RecipeCategory> RecipeCategory { get; set; }
        public ICollection<FeedCategory> FeedCategory { get; set; }
        [NotMapped]
        public TopicType TopicType { get => TopicType.Category; }
    }
}
