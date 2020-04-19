using Eyon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Models.Category>
    {
        IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetCategoryListForDropDown();
        IEnumerable<Category> Search(string query, string includeProperties = null);

        void Update(Category category);
        //Task<int> UpdateAsync( Category category );
    }
}
