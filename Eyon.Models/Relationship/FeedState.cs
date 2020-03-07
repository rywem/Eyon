using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class FeedState
    {
        public long FeedId { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public long StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
