using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eyon.Models.Enums;
using Eyon.Models.Relationship;

namespace Eyon.Models
{
    public class Topic : IRecord, IDynamicForeignRelationship, INamed, IPrivacy
    {
        [Key]
        public long Id { get; set; }
        [Required]        
        [MaxLength(100)]
        [StringLength(100)]        
        public string Name { get; set; }

        [Required]
        public long ObjectId { get; set; }
        [Required]
        public TopicType TopicType { get; set; }
        public Privacy Privacy { get; set; }
        public ICollection<FeedTopic> FeedTopic { get; set; }
    }
}
