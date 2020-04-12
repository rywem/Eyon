using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    /// <summary>
    /// To be applied to ViewModel that's base object is a FeedItem
    /// </summary>
    public interface IFeedItemViewModel
    {
        FeedItemViewModel ToFeedItemViewModel();
    }
}
