using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<SelectListItem> GetCountryListForDropDown()
        {            
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return _db.Country.Select(m => new SelectListItem()
            {
                Text = ti.ToTitleCase(m.Name.ToLower()),
                Value = m.Id.ToString()
            });
        }
    }
}
