using Eyon.DataAccess.Data.Repository.IRepository;

namespace Eyon.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ISiteImageRepository SiteImage { get; private set; }

        public ICookbookRepository Cookbook { get; private set; }

        public ICookbookCategoriesRepository CookbookCategories { get; private set; }

        public ICommunityCookbooksRepository CommunityCookbooks { get; private set; }

        public ICommunityRepository Community { get; private set; }

        public IStateRepository State { get; private set; }
        public ICountryRepository Country { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            this.Category = new CategoryRepository(this._db);
            this.SiteImage = new SiteImageRepository(this._db);
            this.Cookbook = new CookbookRepository(this._db);
            this.Community = new CommunityRepository(this._db);
            this.CommunityCookbooks = new CommunityCookbooksRepository(this._db);
            this.CookbookCategories = new CookbookCategoriesRepository(this._db);
            this.Country = new CountryRepository(this._db);
            this.State = new StateRepository(this._db);
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            this._db.Dispose();
        }
    }
}