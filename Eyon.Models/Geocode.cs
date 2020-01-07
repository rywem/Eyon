using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Geocode
    {
        public long Id { get; set; }
        [MaxLength(20)]
        [StringLength(20)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Latitude { get; set; }
        [MaxLength(20)]
        [StringLength(20)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Longitude { get; set; }
        public ICollection<CommunityGeocode> CommunityGeocode { get; set; }
        public ICollection<PostalCodeGeocode> PostalCodeGeocode { get; set; }
    }
}
