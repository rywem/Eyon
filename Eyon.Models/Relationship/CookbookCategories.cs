using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models.Relationship
{
    public class CookbookCategories
    {
        public long CookbookId { get; set; }
        [ForeignKey("CookbookId")]
        public Cookbook Cookbook { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
