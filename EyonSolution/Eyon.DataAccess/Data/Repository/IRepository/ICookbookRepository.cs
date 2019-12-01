using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICookbookRepository : IRepository<Models.Cookbook>
    {        
        void Update(Cookbook cookbook);
    }    
}
