using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        IEnumerable<SelectListItem> GetCountryListForDropDown();
    }
}
