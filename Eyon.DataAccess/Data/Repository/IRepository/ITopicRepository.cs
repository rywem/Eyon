using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models;
using Eyon.Models.Interfaces;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        Topic AddFromITopicItem( ITopicItem entity );
    }
}
