using Eyon.DataAccess.Data.Repository.IRepository;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository;
using System;

namespace Eyon.DataAccess.Data{
    public class UnitOfWork : IUnitOfWork
    {
        #region getters and setters
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
        public IRecipeUserImageRepository RecipeUserImage { get; private set; }
        public IApplicationUserUserImageRepository ApplicationUserUserImage { get; private set; }
        public IUserImageRepository UserImage { get; private set; }

        public IIngredientRepository Ingredient { get; private set; }
        public IInstructionRepository Instruction { get; private set; }
        public ICookbookRecipeRepository CookbookRecipe { get; private set; }
        public IApplicationUserRecipeRepository ApplicationUserRecipe { get; private set; }

        public ICommunityGeocodeRepository CommunityGeocode { get; private set; }
        public IPostalCodeGeocodeRepository PostalCodeGeocode { get; private set; }
        public IGeocodeRepository Geocode { get; private set; }
        public ICommunityPostalCodeRepository CommunityPostalCode { get; private set; }
        public ICommunityWebReferenceRepository CommunityWebReference { get; private set; }
        public IWebReferenceRepository WebReference { get; private set; }
        public ICommunityRecipeRepository CommunityRecipe { get; private set; }
        public IPostalCodeRepository PostalCode { get; private set; }
        #endregion
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            this.Recipe = new RecipeRepository(this._db);
            this.RecipeCategory = new RecipeCategoryRepository(this._db);            
            //this.RecipeSiteImage = new RecipeSiteImageRepository(this._db);
            this.Ingredient = new IngredientRepository(this._db);
            this.Instruction = new InstructionRepository(this._db);
            this.CookbookRecipe = new CookbookRecipeRepository(this._db);
            this.CommunityGeocode = new CommunityGeocodeRepository(this._db);
            this.PostalCodeGeocode = new PostalCodeGeocodeRepository(this._db);
            this.Geocode = new GeocodeRepository(this._db);
            this.CommunityPostalCode = new CommunityPostalCodeRepository(this._db);
            this.CommunityWebReference = new CommunityWebReferenceRepository(this._db);
            this.WebReference = new WebReferenceRepository(this._db);
            this.PostalCode = new PostalCodeRepository(this._db);

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
            this.CommunityRecipe = new CommunityRecipeRepository(this._db);
            this.ApplicationUserRecipe = new ApplicationUserRecipeRepository(this._db);
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._db.Dispose();
        }
    }
}