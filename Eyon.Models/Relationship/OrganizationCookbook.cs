using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class OrganizationCookbook
    {        
        public long CookbookId { get; set; }
        [ForeignKey("CookbookId")]
        public Cookbook Cookbook { get; set; }
        public long OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }        
    }
}
