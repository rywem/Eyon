using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class CookbookViewModel
    {
        public Cookbook Cookbook { get; set; }
        public List<Category> Categories { get; set; }
        public List<Community> Communities { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "At least one category is required.")]
        [Display(Name ="Selected Category(s)")]        
        public string CategoryIds { get; set; }

        public string CategoryNames { get; set; }
        public CookbookViewModel()
        {
            this.Categories = new List<Category>();
            this.Communities = new List<Community>();
            this.Cookbook = new Cookbook();
        }


        public void SetCategoryIds()
        {
            if(Categories != null && Categories.Count > 0 )
            {
                foreach (var cat in Categories)
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
            if (Categories != null && Categories.Count > 0)
            {
                foreach (var cat in Categories)
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
