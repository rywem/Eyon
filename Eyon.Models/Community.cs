using Eyon.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Community
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
        public ICollection<OrganizationCommunities> OrganizationCommunities { get; set; }
        public ICollection<CommunityWebReference> CommunityWebReferences { get; set; }
        public ICollection<CommunityRecipe> CommunityRecipes { get; set; }
        public ICollection<CommunityPostalCode> CommunityPostalCodes { get; set; }
        public ICollection<CommunityGeocode> CommunityGeocodes { get; set; }
    }
}
