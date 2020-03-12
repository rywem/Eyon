using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        private readonly ApplicationDbContext _db;

        public StateRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public IEnumerable<Models.SiteObjects.SelectBoxItem> GetStateListForDropDown(long countryId)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return _db.State.Where(i => i.CountryId == countryId).Select(m => new Models.SiteObjects.SelectBoxItem()
            {
                Text = ti.ToTitleCase(m.LocalName.ToLower()),
                Value = m.Id.ToString()
            });
        }
    }
}
