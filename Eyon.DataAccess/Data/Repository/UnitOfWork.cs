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

        public ICommunityCookbookRepository CommunityCookbooks { get; private set; }

        public ICommunityRepository Community { get; private set; }

        public IStateRepository State { get; private set; }
        public ICountryRepository Country { get; private set; }

        public ICommunityStateRepository CommunityState { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrganizationCommunitiesRepository OrganizationCommunities { get; private set; }
        public IOrganizationRepository Organization { get; private set; }
        public IOrganizationCookbooksRepository OrganizationCookbooks { get; private set; }

        public IRecipeRepository Recipe { get; private set; }
        public IRecipeCategoryRepository RecipeCategory { get; private set; }
        public IRecipeIngredientRepository RecipeIngredient { get; private set; }
        public IRecipeSiteImageRepository RecipeSiteImage { get; private set; }

        public IIngredientRepository Ingredient { get; private set; }
        public IInstructionRepository Instruction { get; private set; }
        public ICookbookRecipeRepository CookbookRecipe { get; private set; }
        //public IOrganizationRecipeRepository OrganizationRecipe { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            this.Recipe = new RecipeRepository(this._db);
            this.RecipeCategory = new RecipeCategoryRepository(this._db);
            this.RecipeIngredient = new RecipeIngredientRepository(this._db);
            this.RecipeSiteImage = new RecipeSiteImageRepository(this._db);
            this.Ingredient = new IngredientRepository(this._db);
            this.Instruction = new InstructionRepository(this._db);
            this.CookbookRecipe = new CookbookRecipeRepository(this._db);            

            this.Category = new CategoryRepository(this._db);
            this.SiteImage = new SiteImageRepository(this._db);
            this.Cookbook = new CookbookRepository(this._db);
            this.Community = new CommunityRepository(this._db);
            this.CommunityCookbooks = new CommunityCookbookRepository(this._db);
            this.CookbookCategories = new CookbookCategoriesRepository(this._db);
            this.Country = new CountryRepository(this._db);
            this.State = new StateRepository(this._db);
            this.ApplicationUser = new ApplicationUserRepository(this._db);
            this.CommunityState = new CommunityStateRepository(this._db);
            this.OrganizationCommunities = new OrganizationCommunitiesRepository(this._db);
            this.OrganizationCookbooks = new OrganizationCookbooksRepository(this._db);
            this.Organization = new OrganizationRepository(this._db);
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