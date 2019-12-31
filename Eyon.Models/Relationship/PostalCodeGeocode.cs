using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class PostalCodeGeocode
    {
        public long GeocodeId { get; set; }
        [ForeignKey("GeocodeId")]
        public Geocode Geocode { get; set; }
        public long PostalCodeId { get; set; }
        [ForeignKey("PostalCodeId")]
        public PostalCode PostalCode { get; set; }
    }
}
