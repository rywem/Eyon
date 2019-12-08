using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
                Text = ti.ToTitleCase(m.Name),
                Value = m.Id.ToString()
            });
        }
    }
}
