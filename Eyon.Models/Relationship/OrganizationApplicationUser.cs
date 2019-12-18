using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models.Relationship
{ 
    /// <summary>
    /// Maps organizations to their controlling users
    /// </summary>
    public class OrganizationApplicationUser
    {
        /*
        public long OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }      
        */
    }
}