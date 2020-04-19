using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Core.Data.Repository
{
    public class CommunityWebReferenceRepository : Repository<CommunityWebReference>, ICommunityWebReferenceRepository
    {
        private readonly ApplicationDbContext _db;

        public CommunityWebReferenceRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public CommunityWebReference AddFromEntities( Community firstEntity, WebReference secondEntity )
        {
            var newObj = new CommunityWebReference()
            {
                CommunityId = firstEntity.Id,
                WebReferenceId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
