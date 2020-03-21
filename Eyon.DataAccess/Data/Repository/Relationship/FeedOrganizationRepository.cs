using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository
{
    public class FeedOrganizationRepository : Repository<FeedOrganization>, IFeedOrganizationRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedOrganizationRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedOrganization entityToAdd )
        {
            if (_db.FeedOrganization.Any(x => x.FeedId == entityToAdd.FeedId && x.OrganizationId == entityToAdd.OrganizationId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  OrganizationId {1}", entityToAdd.FeedId, entityToAdd.OrganizationId)));

            base.Add(entityToAdd);
        }

        public FeedOrganization AddFromEntities( Feed firstEntity, Organization secondEntity )
        {
            var newObj = new FeedOrganization()
            {
                FeedId = firstEntity.Id,
                OrganizationId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
