using System;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
