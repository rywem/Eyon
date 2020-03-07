using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class FeedCountry
    {
        public long FeedId { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}
