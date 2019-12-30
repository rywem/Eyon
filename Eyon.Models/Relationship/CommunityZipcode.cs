using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class CommunityZipcode
    {
        public long Id { get; set; }
        public string ZipCode { get; set; }
        public long CommunityId { get; set; }        
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
    }
}
