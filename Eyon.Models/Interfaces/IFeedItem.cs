using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IFeedItem : IRecord, ICreated, IModified, IPrivacy, IDescription, INamed
    {
    }
}
