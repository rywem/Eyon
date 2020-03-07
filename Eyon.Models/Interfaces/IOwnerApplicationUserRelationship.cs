using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    /// <summary>
    /// Use this interface when a read/write permissions need to be controlled.
    /// </summary>
    public interface IOwnerApplicationUserRelationship : IDynamicForeignRelationship
    {        
        string ApplicationUserId { get; set; }
        ApplicationUser ApplicationUser { get; set; }
    }
}
