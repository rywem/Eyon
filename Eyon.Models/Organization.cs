using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Organization : IHasOwners<ApplicationUserOrganization>, ITopicItem
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        public string Website { get; set; }
        public ICollection<OrganizationCommunity> OrganizationCommunity { get; set; }
        public ICollection<OrganizationCookbook> OrganizationCookbook { get; set; }
        public ICollection<ApplicationUserOrganization> ApplicationUserOwner { get; set; }
        public ICollection<FeedOrganization> FeedOrganization { get; set; }
        [NotMapped]
        public TopicType TopicType { get => TopicType.Organization; }
    }
}
