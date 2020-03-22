using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class FeedItemViewModel : IFeedItem
    {
        public long Id { get; set; }
        
        public string Description { get; set; }

        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public Privacy Privacy { get; set; }

        public List<Topic> Topics { get; set; }
    }
}
