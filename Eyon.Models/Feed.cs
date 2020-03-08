﻿using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models
{
    public class Feed : ICreated, IModified, IPrivacy, IHasOwners<ApplicationUserFeed>
    {
        [Key]
        public long Id { get; set; }
        
        [MaxLength(600)]
        [StringLength(600)]        
        public string Text { get; set; }
        public ICollection<ApplicationUserFeed> ApplicationUserOwner { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public Privacy Privacy { get; set; }

        public ICollection<FeedCommunity> FeedCommunity { get; set; }
        public ICollection<FeedState> FeedState { get; set; }
        public ICollection<FeedOrganization> FeedOrganization { get; set; }
        public ICollection<FeedCategory> FeedCategory { get; set; }
        public ICollection<FeedCountry> FeedCountry { get; set; }
        public ICollection<FeedCookbook> FeedCookbook { get; set; }
        public ICollection<FeedRecipe> FeedRecipe { get; set; }
        public ICollection<FeedProfile> FeedUser { get; set; }
        public ICollection<FeedTopic> FeedTopic { get; set; }

    }
}