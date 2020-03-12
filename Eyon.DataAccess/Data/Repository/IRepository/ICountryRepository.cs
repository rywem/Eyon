using System.Collections.Generic;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<Models.SiteObjects.SelectBoxItem> GetCountryListForDropDown();
    }
}
