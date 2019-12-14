using System;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ISiteImageRepository SiteImage { get; }
        ICookbookRepository Cookbook { get; }
        ICookbookCategoriesRepository CookbookCategories { get; }
        ICommunityCookbooksRepository CommunityCookbooks { get; }
        ICommunityRepository Community { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        IDatabaseTransaction BeginTransaction();
        void Save();        
    }
}
