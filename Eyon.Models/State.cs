using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class State : ITopicType, INamed
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        public string Type { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        public string LocalName { get; set; }        
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<CommunityState> CommunityState { get; set; }
        public ICollection<FeedState> FeedState { get; set; }
        [NotMapped]
        public TopicType Topic { get => TopicType.State; }
    }
}
