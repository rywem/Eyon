using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IManyToManyRelationshipRepository<TRelationEntity, TFirstEntity, TSecondEntity> : IRepository<TRelationEntity>
        where TRelationEntity : class
        where TFirstEntity : class
        where TSecondEntity : class
    {
        TRelationEntity AddFromEntities( TFirstEntity firstEntity, TSecondEntity secondEntity );
    }
}
