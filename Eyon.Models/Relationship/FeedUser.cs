using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    /// <summary>
    /// This class allows for the following of other ApplicationUser and controls populating the feed.
    /// </summary>
    /// <remarks>
    /// Do not confuse FeedUser (feed) class with ApplicationUserFeed (security access).
    /// </remarks>
    public class FeedUser
    {
        public long FeedId { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public long ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
