﻿using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }       
        
        public ICollection<ApplicationUserRecipe> ApplicationUserRecipe { get; set; }
        public ICollection<ApplicationUserCookbook> ApplicationUserCookbook { get; set; }
        public ICollection<ApplicationUserUserImage> ApplicationUserUserImage { get; set; }
        public ICollection<ApplicationUserOrganization> ApplicationUserOrganization { get; set; }

        public ICollection<ApplicationUserProfile> ApplicationUserProfile { get; set; }
        /// <summary>
        /// Security access
        /// </summary>
        public ICollection<ApplicationUserFeed> ApplicationUserFeed { get; set; }
        public Profile Profile { get; set; }
    }
}
