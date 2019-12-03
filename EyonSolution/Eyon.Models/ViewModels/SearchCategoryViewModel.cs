using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    //https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-3.0
    public class SearchCategoryViewModel
    {
        public List<Category> ResultsCategories { get; set; }
        public List<Category> SelectedCategories { get; set; }
        public string SearchString { get; set; }

        public SearchCategoryViewModel()
        {
            this.ResultsCategories = new List<Category>();
            this.SelectedCategories = new List<Category>();
        }
    }
}
