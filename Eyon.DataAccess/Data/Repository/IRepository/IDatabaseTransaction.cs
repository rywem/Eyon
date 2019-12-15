using System;


namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
