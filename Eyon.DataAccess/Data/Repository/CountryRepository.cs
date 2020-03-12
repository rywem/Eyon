using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eyon.DataAccess.Data.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;

        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public IEnumerable<Models.SiteObjects.SelectBoxItem> GetCountryListForDropDown()
        {            
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return _db.Country.Select(m => new Models.SiteObjects.SelectBoxItem()
            {
                Text = ti.ToTitleCase(m.Name.ToLower()),
                Value = m.Id.ToString()
            });
        }
    }
}
