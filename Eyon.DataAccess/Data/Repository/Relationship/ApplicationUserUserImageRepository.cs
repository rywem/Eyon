using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class ApplicationUserUserImageRepository : Repository<ApplicationUserUserImage>, IApplicationUserUserImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserUserImageRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
