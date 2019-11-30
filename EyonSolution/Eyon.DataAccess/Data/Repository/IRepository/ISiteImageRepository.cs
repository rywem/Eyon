using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ISiteImageRepository : IRepository<Models.SiteImage>
    {
        void Update(Models.SiteImage image);
    }    
    
}
