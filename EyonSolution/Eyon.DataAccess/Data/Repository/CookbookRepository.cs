using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eyon.DataAccess.Data.Repository
{
    public class CookbookRepository : Repository<Cookbook>, ICookbookRepository
    {
        private readonly ApplicationDbContext _db;

        public CookbookRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }


        public void Update(Cookbook cookbook)
        {
            var objFromDb = _db.Cookbook.FirstOrDefault(s => s.Id == cookbook.Id);
            objFromDb.Name = cookbook.Name;
            objFromDb.Copyright = cookbook.Copyright;
            objFromDb.Author = cookbook.Author;
            objFromDb.Description = cookbook.Description;
            objFromDb.ISBN = cookbook.ISBN;            
            _db.SaveChanges();
        }
        
    }
}
