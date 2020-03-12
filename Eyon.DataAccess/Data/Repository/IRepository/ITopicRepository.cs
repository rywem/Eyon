using System.Collections.Generic;
using Eyon.Models;
using Eyon.Models.Interfaces;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ITopicRepository<T> : IRepository<Topic>
        where T : ITopicItem
    {
        Topic AddFromEntity( T entity);
    }
}
