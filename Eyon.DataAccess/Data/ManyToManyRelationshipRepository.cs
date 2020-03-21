using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data
{
    public abstract class ManyToManyRelationshipRepository<TManyToMany, TFirstEntity, TSecondEntity> : Repository<TManyToMany>, IManyToManyRelationshipRepository<TManyToMany, TFirstEntity, TSecondEntity>
        where TManyToMany : class
        where TFirstEntity : class
        where TSecondEntity : class
    {
        public ManyToManyRelationshipRepository( DbContext context ) : base(context)
        {

        }
        public abstract TManyToMany AddFromEntities( TFirstEntity firstEntity, TSecondEntity secondEntity );        
    }
}
