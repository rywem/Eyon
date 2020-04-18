using Eyon.Models.Interfaces;
using Eyon.Models.SiteObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eyon.Models.ViewModels
{
    public class CookbookViewModel : IFeedItemViewModel
    {
        public Cookbook Cookbook { get; set; }        
        public List<Community> Community { get; set; }
        public ListItemSelector<Category> CategorySelector { get; set; }

        public CookbookViewModel()
        {
            this.Community = new List<Community>();
            this.Cookbook = new Cookbook();
            this.CategorySelector = new ListItemSelector<Category>("Category");
        }

        public FeedItemViewModel ToFeedItemViewModel( Feed feed = null )
        {
            FeedItemViewModel feedItemViewModel = new FeedItemViewModel();
            if ( Community != null && Community.Count > 0 )
                feedItemViewModel.Communities.AddRange(Community);
            if ( CategorySelector.Items != null && CategorySelector.Items.Count > 0 )
                feedItemViewModel.Categories.AddRange(CategorySelector.Items);
            feedItemViewModel.Cookbooks.Add(this.Cookbook);
            feedItemViewModel.FeedItem = this.Cookbook;
            if ( feed != null )
                feedItemViewModel.Feed = feed;

            return feedItemViewModel;
        }
    }
}
