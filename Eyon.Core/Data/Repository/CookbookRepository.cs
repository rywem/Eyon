using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository
{
    public class CookbookRepository : PrivacyRepository<Cookbook, ApplicationUserCookbook>, ICookbookRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( Cookbook entity )
        {
            entity.CreationDateTime = DateTime.Now.ToUniversalTime();
            entity.ModifiedDateTime = entity.CreationDateTime;
            base.Add(entity);
        }

        public void Update(Cookbook cookbook)
        {
            var objFromDb = _db.Cookbook.FirstOrDefault(s => s.Id == cookbook.Id);
            objFromDb.Name = cookbook.Name;
            objFromDb.Copyright = cookbook.Copyright;
            objFromDb.Author = cookbook.Author;
            objFromDb.Description = cookbook.Description;
            objFromDb.ISBN = cookbook.ISBN;
            objFromDb.ModifiedDateTime = DateTime.Now.ToUniversalTime();
            cookbook.ModifiedDateTime = objFromDb.ModifiedDateTime;
            dbSet.Update(objFromDb);
        }

        public void UpdateIfOwner( string currentUserId, Cookbook cookbook )
        {
            var objFromDb = ( from r in _db.Cookbook
                              join a in _db.ApplicationUserCookbook on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == cookbook.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  cookbook.Id {1}", currentUserId, cookbook.Id)));
            
            objFromDb.Name = cookbook.Name;
            objFromDb.Copyright = cookbook.Copyright;
            objFromDb.Author = cookbook.Author;
            objFromDb.Description = cookbook.Description;
            objFromDb.ISBN = cookbook.ISBN;
            objFromDb.ModifiedDateTime = DateTime.Now.ToUniversalTime();
            cookbook.ModifiedDateTime = objFromDb.ModifiedDateTime;
            dbSet.Update(objFromDb);
        }
    }
}
