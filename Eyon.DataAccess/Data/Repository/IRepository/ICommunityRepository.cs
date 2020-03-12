using Eyon.Models;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICommunityRepository : IRepository<Models.Community>
    {
        IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetCategoryListForDropDown();
        IEnumerable<Community> Search( string searchString, string includeProperties = null );
        void Update(Community community);
    }    
}
