using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IOwner
    {
        long ObjectId { get; set; }
        string ApplicationUserId { get; set; }
        ApplicationUser ApplicationUser { get; set; }
    }
}
