using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICommunityRepository : IRepository<Models.Community>
    {
        IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetCategoryListForDropDown();

        void Update(Community community);
    }    
}
