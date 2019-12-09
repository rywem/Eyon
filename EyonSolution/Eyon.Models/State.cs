using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class State
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        [Required]
        public string Type { get; set; }
        public string LocalName { get; set; }        
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<CommunityState> CommunityState { get; set; }
    }
}
