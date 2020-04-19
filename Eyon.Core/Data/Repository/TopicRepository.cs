using Eyon.Core.Data.Repository.IRepository;
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

namespace Eyon.Core.Data.Repository
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

        /// <summary>
        /// Adds a topic object from an ITopicItem entity.
        /// </summary>
        /// <param name="entity">A class that inherits from ITopicItem</param>
        /// <returns>The new Topic object</returns>
        public Topic AddFromITopicItem( ITopicItem entity )
        {
            Topic topic = new Topic();
            topic.Name = entity.Name;
            topic.ObjectId = entity.Id;
            topic.TopicType = entity.TopicType;
            base.Add(topic);
            return topic;
        }

        public void UpdateFromITopicItem( ITopicItem entity )
        {
            var objFromDb = _db.Topic.FirstOrDefault(x => x.ObjectId == entity.Id && x.TopicType == entity.TopicType);

            if ( objFromDb == null )
                throw new SafeException(Models.Enums.ErrorType.AnErrorOccurred, new Exception(string.Format("ITopicItem not found ObjectId {0} TopicType {1}", entity.Id, entity.TopicType)));

            objFromDb.Name = entity.Name;
            dbSet.Update(objFromDb);
        }

        public Topic FirstOrDefaultFromITopicItem( ITopicItem entity )
        {
            return _db.Topic.FirstOrDefault(x => x.ObjectId == entity.Id && x.TopicType == entity.TopicType);
        }

        public void RemoveFromITopicItem(ITopicItem entity )
        {
            var objFromDb = _db.Topic.FirstOrDefault(x => x.ObjectId == entity.Id && x.TopicType == entity.TopicType);
            dbSet.Remove(objFromDb);
        }
    }
}
