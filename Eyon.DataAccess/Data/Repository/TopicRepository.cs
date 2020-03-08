﻿using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository
{
    /// <summary>
    /// FeedRepository inherits from PrivacyRepository. 
    /// </summary>
    /// <remarks>
    /// Privacy information stored on the feed object is a denormalization 
    /// of data and is not the primary information location. Privacy 
    /// information is also stored on the base object's table. 
    /// </remarks>
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        private readonly ApplicationDbContext _db;

        public TopicRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }
    }
}
