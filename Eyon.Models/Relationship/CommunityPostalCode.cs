using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class CommunityPostalCode
    {
        public long CommunityId { get; set; }
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
        public long PostalCodeId { get; set; }
        [ForeignKey("PostalCodeId")]
        public PostalCode PostalCode { get; set; }
    }
}
