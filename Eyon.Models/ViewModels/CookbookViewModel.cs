using Eyon.Models.SiteObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eyon.Models.ViewModels
{
    public class CookbookViewModel
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
    }
}
