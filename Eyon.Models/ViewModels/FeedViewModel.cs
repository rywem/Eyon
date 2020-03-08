using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class FeedViewModel
    {
        public List<Feed> Feed { get; set; }

        public FeedViewModel()
        {
            this.Feed = new List<Feed>();
        }
    }
}
