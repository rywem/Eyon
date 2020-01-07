using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eyon.Models.ViewModels
{
    public class CookbookViewModel
    {
        public Cookbook Cookbook { get; set; }
        public List<Category> Category { get; set; }
        public List<Community> Community { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "At least one category is required.")]
        [Display(Name ="Selected Category(s)")]        
        public string CategoryIds { get; set; }

        public string CategoryNames { get; set; }
        public CookbookViewModel()
        {
            this.Category = new List<Category>();
            this.Community = new List<Community>();
            this.Cookbook = new Cookbook();
        }


        public void SetCategoryIds()
        {
            if(Category != null && Category.Count > 0 )
            {
                foreach (var cat in Category)
                {
                    if (string.IsNullOrEmpty(CategoryIds))
                    {
                        CategoryIds += cat.Id.ToString();
                    }
                    else
                    {
                        CategoryIds += string.Format(",{0}", cat.Id);
                    }
                }
            }
        }
        public void SetCategoryNames()
        {
            if (Category != null && Category.Count > 0)
            {
                foreach (var cat in Category)
                {
                    if (string.IsNullOrEmpty(CategoryIds))
                    {
                        CategoryNames += string.Format("#{0}", cat.Name);
                    }
                    else
                    {
                        CategoryNames += string.Format(" #{0}", cat.Name);
                    }
                }
            }
        }
    }
}
