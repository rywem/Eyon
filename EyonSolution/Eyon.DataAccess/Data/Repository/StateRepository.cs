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
    public class StateRepository : Repository<State>, IStateRepository
    {
        private readonly ApplicationDbContext _db;

        public StateRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public IEnumerable<SelectListItem> GetStateListForDropDown(long countryId)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return _db.State.Where(i => i.CountryId == countryId).Select(m => new SelectListItem()
            {
                Text = ti.ToTitleCase(m.LocalName),
                Value = m.Id.ToString()
            });
        }
    }
}
