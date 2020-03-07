using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IOwnerApplicationUserRelationship : IRelationship
    {        
        string ApplicationUserId { get; set; }
        ApplicationUser ApplicationUser { get; set; }
    }
}
