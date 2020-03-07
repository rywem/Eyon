using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eyon.Models.Enums;

namespace Eyon.Models
{
    public class Topic : IRecord, IRelationship, INamed
    {
        [Key]
        public long Id { get; set; }
        [Key]
        public string Name { get; set; }

        [Required]
        public long ObjectId { get; set; }
        [Required]
        public TopicType TopicType { get; set; }
    }
}
