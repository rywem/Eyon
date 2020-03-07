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
    public class Profile : IRecord, INamed, ICreated, IModified, ITopicType
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public TopicType Topic => TopicType.Profile;
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<FeedProfile> FeedProfile { get; set; }


    }
}
