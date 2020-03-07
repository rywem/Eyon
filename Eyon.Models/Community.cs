using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Community : ITopicType
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }        
        public bool Active { get; set; } = false;        
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<CommunityCookbook> CommunityCookbooks { get; set; }

        public CommunityState CommunityState { get; set; }
        public ICollection<OrganizationCommunity> OrganizationCommunity { get; set; }
        public ICollection<CommunityWebReference> CommunityWebReference { get; set; }
        public ICollection<CommunityRecipe> CommunityRecipe { get; set; }
        public ICollection<CommunityPostalCode> CommunityPostalCode { get; set; }
        public ICollection<CommunityGeocode> CommunityGeocode { get; set; }
        [NotMapped]
        public TopicType Topic { get => TopicType.Community; }
    }
}
