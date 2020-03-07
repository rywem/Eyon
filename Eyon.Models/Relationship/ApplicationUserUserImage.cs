using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class ApplicationUserUserImage : IOwnerApplicationUserRelationship
    {
        public long ObjectId { get; set; }
        [ForeignKey("ObjectId")]
        public UserImage UserImage { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
