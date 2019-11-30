using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }        
        ISiteImageRepository SiteImage { get; }
        void Save();
        bool SaveTransaction();
    }
}
