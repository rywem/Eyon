using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    /// <summary>
    /// ApplicationUserFeed class controls access security. 
    /// </summary>
    /// <remarks>
    /// Follow Profiles, ApplicationUsers for Security
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
