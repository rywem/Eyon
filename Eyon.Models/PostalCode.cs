using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class PostalCode
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(20)]
        [StringLength(20)]        
        public string Text { get; set; }
        [Required]        
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<CommunityPostalCode> CommunityPostalCodes { get; set; }
        public ICollection<PostalCodeGeocode> PostalCodeGeocodes { get; set; }
    }
}
