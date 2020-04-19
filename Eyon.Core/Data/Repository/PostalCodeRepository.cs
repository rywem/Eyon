using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eyon.Core.Data.Repository
{
    public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
    {
        private readonly ApplicationDbContext _db;

        public PostalCodeRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        //public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        //{
        //    return _db.Category.Select(i => new SelectListItem()
        //    {
        //        Text = i.Name,
        //        Value = i.Id.ToString()
        //    });
        //}

        //public IEnumerable<Community> Search( string searchString, string includeProperties = null )
        //{
        //    //IQueryable<Community> query
        //    /*var query = (from c in _db.Community
        //                join cs in _db.CommunityState
        //                on c.Id equals cs.CommunityId into csc
        //                from csci in csc.DefaultIfEmpty()
        //                join s in _db.State
        //                on csci.StateId equals s.Id into stateCS
        //                from scsc in stateCS.DefaultIfEmpty()
        //                where ( c.Name.Contains(searchString)
        //                    || scsc.Name.Contains(searchString)
        //                    || scsc.Code.Contains(searchString)
        //                    || scsc.LocalName.Contains(searchString) )
        //                select c);
        //    */
        //    return null;

        //}

        //public void Update(Community community)
        //{
        //    var objFromDb = _db.Community.FirstOrDefault(s => s.Id == community.Id);
        //    objFromDb.Name = community.Name;
        //    objFromDb.Active = community.Active;
        //    _db.SaveChanges();
        //}
    }
}
