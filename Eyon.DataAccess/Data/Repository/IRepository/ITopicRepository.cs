using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models;
using Eyon.Models.Interfaces;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ITopicRepository<T> : IRepository<Topic>
        where T : ITopicItem
    {
        Topic AddFromITopicItem( T entity);
    }
}
