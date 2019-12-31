using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class CommunityGeocode
    {
        public long CommunityId { get; set; }
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
        public long GeocodeId { get; set; }
        [ForeignKey("GeocodeId")]
        public Geocode Geocode { get; set; }
    }
}
