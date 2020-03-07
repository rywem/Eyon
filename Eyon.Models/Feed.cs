using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models
{
    public class Feed : ICreated, IModified, IPrivacy, IHasOwners<ApplicationUserFeed>
    {
        public long Id { get; set; }
        public ICollection<ApplicationUserFeed> ApplicationUserOwner { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public Privacy Privacy { get; set; }
    }
}
