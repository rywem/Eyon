using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<SelectListItem> GetCountryListForDropDown();
    }
}
