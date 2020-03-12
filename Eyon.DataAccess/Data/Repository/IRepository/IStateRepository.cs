using Eyon.Models;
using System.Collections.Generic;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
        IEnumerable<Models.SiteObjects.SelectBoxItem> GetStateListForDropDown(long countryId);
    }
}
