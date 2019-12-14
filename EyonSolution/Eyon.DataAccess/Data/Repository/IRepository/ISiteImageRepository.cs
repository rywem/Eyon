using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface ISiteImageRepository : IRepository<SiteImage>
    {
        void Update(Models.SiteImage image);
    }    
    
}
