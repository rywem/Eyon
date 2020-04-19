using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<SelectListItem> GetCountryListForDropDown();
    }
}
