using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class CommunityWebReference
    {
        public long CommunityId { get; set; }
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WebReferenceId { get; set; }
        [ForeignKey("WebReferenceId")]
        public WebReference WebReference { get; set; }
    }
}
