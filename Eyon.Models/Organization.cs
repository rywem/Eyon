﻿using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models
{
    public class Organization : IRecord, IHasOwners<ApplicationUserOrganization>
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
        public ICollection<OrganizationCommunity> OrganizationCommunity { get; set; }
        public ICollection<OrganizationCookbook> OrganizationCookbook { get; set; }
        public ICollection<ApplicationUserOrganization> ApplicationUserOwner { get; set; }
    }
}
