using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models.Relationship
{
    public class CommunityState
    {
        public long CommunityId { get; set; }
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
        public long StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
