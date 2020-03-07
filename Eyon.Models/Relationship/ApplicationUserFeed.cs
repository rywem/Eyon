using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    /// <summary>
    /// ApplicationUserFeed class controls security. 
    /// </summary>
    /// <remarks>
    /// Do not confuse this class with FeedUsers, which 
    /// controls populating the feed with user activity.
    /// </remarks>
    public class ApplicationUserFeed : IOwnerApplicationUserRelationship
    {
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public long ObjectId { get; set; }
        [ForeignKey("ObjectId")]
        public Feed Feed { get; set; }
    }
}
