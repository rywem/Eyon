﻿using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models
{
    public class Organization
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        public string Website { get; set; }
        public ICollection<OrganizationsCommunities> OrganizationCommunities { get; set; }
        public ICollection<OrganizationCookbooks> OrganizationCookbooks { get; set; }
        public ICollection<OrganizationApplicationUser> OrganizationApplicationUsers { get; set; }
        //public ICollection<OrganizationRecipes> OrganizationRecipes { get; set; }

    }
}
