using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class FeedViewModel
    {
        public List<FeedItemViewModel> FeedItems { get; set; }

        public FeedViewModel()
        {
            this.FeedItems = new List<FeedItemViewModel>();
        }
    }
}
