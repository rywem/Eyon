using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class Cookbook : IHasOwners<ApplicationUserCookbook>, ICreated, IModified, IPrivacy, INamed, ITopicType
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        public string Name { get; set; }
        [MaxLength(3000)]
        [StringLength(3000)]
        public string Description { get; set; }
        [MaxLength(200)]
        [StringLength(200)]
        public string Author { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        public string Copyright { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        public string ISBN { get; set; }
        public Privacy Privacy { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public ICollection<Eyon.Models.Relationship.CommunityCookbook> CommunityCookbook { get; set; }
        public ICollection<CookbookCategories> CookbookCategory { get; set; }

        public ICollection<OrganizationCookbook> OrganizationCookbook { get; set; }
        public ICollection<CookbookRecipe> CookbookRecipe { get; set; }
        public ICollection<ApplicationUserCookbook> ApplicationUserOwner { get; set; }

        [NotMapped]
        public TopicType Topic { get => TopicType.Cookbook; }
        //public ICollection<CookbookApplicationUsers> CookbookApplicationUsers { get; set; }
        //public ICollection<CookbookSiteImages> CookbookSiteImages { get; set; }


    }
}
