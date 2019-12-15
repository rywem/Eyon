using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models.Relationship
{
    public class CommunityCookbooks
    {
        public long CookbookId { get; set; }
        [ForeignKey("CookbookId")]
        public Cookbook Cookbook { get; set; }
        public long CommunityId { get; set; }
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
    }
}
