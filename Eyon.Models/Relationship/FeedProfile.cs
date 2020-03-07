using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    /// <summary>
    /// This class allows for populating the feed.
    /// </summary>
    /// <remarks>
    /// Follow Profiles, ApplicationUsers for Security
    /// </remarks>
    public class FeedProfile
    {
        public long FeedId { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public long ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }
}
