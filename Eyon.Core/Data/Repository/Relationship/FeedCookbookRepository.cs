﻿using Eyon.Core.Data.Repository.IRepository;
using System;
using System.Linq;
using Eyon.Models.Relationship;
using Eyon.Models.Errors;
using Eyon.Models;

namespace Eyon.Core.Data.Repository
{
    public class FeedCookbookRepository : Repository<FeedCookbook>, IFeedCookbookRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedCookbookRepository( ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public override void Add( FeedCookbook entityToAdd )
        {
            if (_db.FeedCookbook.Any(x => x.FeedId == entityToAdd.FeedId && x.CookbookId == entityToAdd.CookbookId) )
                throw new SafeException("An error ocurred.", new Exception(string.Format("FeedCategory already exists. FeedId {0},  CookbookId {1}", entityToAdd.FeedId, entityToAdd.CookbookId)));

            base.Add(entityToAdd);
        }

        public FeedCookbook AddFromEntities( Feed firstEntity, Cookbook secondEntity )
        {
            var newObj = new FeedCookbook()
            {
                FeedId = firstEntity.Id,
                CookbookId = secondEntity.Id
            };
            base.Add(newObj);
            return newObj;
        }
    }
}
