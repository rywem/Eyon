using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Abstraction
{
    public class ListItemUpdater<TRelationshipEntity, TFirstEntity, TSecondEntity>
    {        
        public IEnumerable<TRelationshipEntity> AddListItems(TFirstEntity baseEntity, List<TSecondEntity> newEntities, 
            Func<TFirstEntity, TSecondEntity, TRelationshipEntity> addFunc,
            Func<TFirstEntity, TSecondEntity, bool> validateFunc = null )
        {

            
            foreach ( var item in newEntities )
            {
                if ( validateFunc != null )
                {
                    if ( validateFunc.Invoke(baseEntity, item) )
                    {
                        yield return addFunc.Invoke(baseEntity, item);
                    }
                }
                else
                    yield return addFunc.Invoke(baseEntity, item);
            }
        }
    }
}
