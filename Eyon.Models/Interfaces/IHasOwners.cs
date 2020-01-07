using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IHasOwners<T> : IRecord
        where T :  IOwner
    {
        ICollection<T> ApplicationUserOwner { get; set; }
    }
}
