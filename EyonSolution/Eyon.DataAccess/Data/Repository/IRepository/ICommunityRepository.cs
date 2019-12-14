using Eyon.Models;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICommunityRepository : IRepository<Models.Community>
    {
        IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetCategoryListForDropDown();

        void Update(Community community);
    }    
}
