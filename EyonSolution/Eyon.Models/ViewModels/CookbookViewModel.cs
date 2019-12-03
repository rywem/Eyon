using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class CookbookViewModel
    {
        public Cookbook Cookbook { get; set; }
        public List<Category> Categories { get; set; }
        public List<Community> Communities { get; set; }        
        public CookbookViewModel()
        {
            this.Categories = new List<Category>();
            this.Communities = new List<Community>();
            this.Cookbook = new Cookbook();
    }
    }
}
