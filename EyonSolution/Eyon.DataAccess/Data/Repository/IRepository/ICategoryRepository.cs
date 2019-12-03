using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Models.Category>
    {
        IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetCategoryListForDropDown();
        IEnumerable<Category> Search(string query, string includeProperties = null);

        void Update(Category category);
    }    
}
