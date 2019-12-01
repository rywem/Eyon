using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Repository
{
    public class CommunityCookbooksRepository : Repository<CommunityCookbooks>, ICommunityCookbooksRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityCookbooksRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
    }
}
