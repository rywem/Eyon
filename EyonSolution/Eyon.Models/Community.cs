using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models
{
    public class Community
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string WikipediaURL { get; set; }
        public string County { get; set; }
        public string StateProvince { get; set; }
        [Required]
        public string Country { get; set; }
        public ICollection<CommunityOrganizations> CommunityOrganizations { get; set; }
        public ICollection<CommunityCookbooks> CommunityCookbooks { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
