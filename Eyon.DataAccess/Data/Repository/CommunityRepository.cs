using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityRepository : Repository<Community>, ICommunityRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public IEnumerable<Community> Search( string searchString, string includeProperties = null )
        {
            //IQueryable<Community> query;
            searchString = searchString.ToUpper();
            var query = (from c in _db.Community
                        join cs in _db.CommunityState
                        on c.Id equals cs.CommunityId into csc
                        from csci in csc.DefaultIfEmpty() // search state
                        join s in _db.State
                        on csci.StateId equals s.Id into stateCS
                        from scsc in stateCS.DefaultIfEmpty()
                        // search postal code
                        join cp in _db.CommunityPostalCodes 
                        on c.Id equals cp.CommunityId into cpc
                        from cpci in cpc.DefaultIfEmpty()
                        join p in _db.PostalCode 
                        on cpci.PostalCodeId equals p.Id into postalCP 
                        from pcpc in postalCP.DefaultIfEmpty()
                        where ( c.Name.Contains(searchString)
                            || scsc.Name.Contains(searchString)
                            || scsc.Code.Contains(searchString)
                            || scsc.LocalName.Contains(searchString) 
                            || pcpc.Text.Contains(searchString) )
                        select c);
            
            if ( includeProperties != null )
            {
                foreach ( var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) )
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsEnumerable();

        }

        public void Update(Community community)
        {
            var objFromDb = _db.Community.FirstOrDefault(s => s.Id == community.Id);
            objFromDb.Name = community.Name;
            objFromDb.Active = community.Active;
            _db.SaveChanges();
        }
    }
}
