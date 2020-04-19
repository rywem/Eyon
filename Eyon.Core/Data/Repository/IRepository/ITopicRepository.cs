using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models;
using Eyon.Models.Interfaces;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        Topic AddFromITopicItem( ITopicItem entity );
        void UpdateFromITopicItem( ITopicItem entity );
        Topic FirstOrDefaultFromITopicItem( ITopicItem entity );
        void RemoveFromITopicItem( ITopicItem entity );
    }
}
