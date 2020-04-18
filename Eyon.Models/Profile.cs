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
    /// <summary>
    /// Profiles attach to the ApplicationUser. Follow Profiles
    /// </summary>
    public class Profile : IHasOwners<ApplicationUserProfile>, ICreated, IModified, ITopicItem, IPrivacy, IFeedItem
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public Privacy Privacy { get; set; }
        public TopicType TopicType => TopicType.Profile;
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<FeedProfile> FeedProfile { get; set; }
        public ICollection<ApplicationUserProfile> ApplicationUserOwner { get; set; }
    }
}
