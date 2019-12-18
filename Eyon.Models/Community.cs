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
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string WikipediaURL { get; set; }
        public string County { get; set; }        
        public bool Active { get; set; } = false;        
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<CommunityCookbooks> CommunityCookbooks { get; set; }

        public CommunityState CommunityState { get; set; }
        public ICollection<OrganizationCommunities> OrganizationCommunities { get; set; }
        //public ICollection<Recipe> Recipes { get; set; }
    }
}
