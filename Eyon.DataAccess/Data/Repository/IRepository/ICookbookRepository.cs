using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ICookbookRepository : IRepository<Cookbook>
    {        
        void Update(Cookbook cookbook);
    }    
}
