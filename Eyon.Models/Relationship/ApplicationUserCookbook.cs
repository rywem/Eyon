using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class ApplicationUserCookbook : IOwnerApplicationUserRelationship
    {
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public long ObjectId { get; set; }
        [ForeignKey("ObjectId")]
        public Cookbook Cookbook { get; set; }
    }
}
