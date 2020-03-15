using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IFeedItem : IRecord, ICreated, IModified, IPrivacy, IDescription
    {
        //long FeedId { get; set; }

        //Feed Feed { get; set; }
    }
}
