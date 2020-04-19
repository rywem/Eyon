using Eyon.Models;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface ISiteImageRepository : IRepository<SiteImage>
    {
        void Update(Models.SiteImage image);
    }    
    
}
