using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Models.Category>
    {
        IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetCategoryListForDropDown();

        void Update(Category category);
    }    
}
