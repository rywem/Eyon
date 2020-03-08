using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Interfaces;
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
    public class TopicRepository : Repository<Topic>, ITopicRepository<ITopicItem>
    {
        private readonly ApplicationDbContext _db;

        public TopicRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        //public void Add( ITopicItem entity )
        //{
        //    throw new NotImplementedException();
        //}

        public void Add( ITopicItem entity )
        {
            Topic topic = new Topic();
            topic.Name = entity.Name;
            topic.ObjectId = entity.Id;
            topic.TopicType = entity.TopicType;
            base.Add(topic);
        }
    }
}
