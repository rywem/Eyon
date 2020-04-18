using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class FeedItemViewModel 
    {
        public IFeedItem FeedItem { get; set; }

        public Feed Feed { get;set; }
        public List<Community> Communities { get; set; }
        //public List<State> States { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<Category> Categories { get; set; }
        public List<Cookbook> Cookbooks { get; set; }
        public List<Recipe> Recipes { get; set; }
        //public List<Country> Country { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<Topic> Topics { get; set; }

        public List<UserImage> UserImages { get; set; }

        public FeedItemViewModel()
        {
            this.Communities = new List<Community>();
            //this.States = new List<State>();
            this.Organizations = new List<Organization>();
            this.Categories = new List<Category>();
            this.Cookbooks = new List<Cookbook>();
            this.Recipes = new List<Recipe>();
            //this.Country = new List<Country>();
            this.Profiles = new List<Profile>();
            this.Topics = new List<Topic>();
            this.UserImages = new List<UserImage>();
        }
    }
}
