using Eyon.DataAccess.Data.Repository.IRepository;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository;
using System;

namespace Eyon.DataAccess.Data{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        #region getters and setters

        /*
        private Lazy<IXRepository> _X;
        public IXRepository X
        {
            get
            {
                return _X.Value;
            }
        }
        */
        private Lazy<ICategoryRepository> _Category;
        public ICategoryRepository Category 
        {
            get
            {
                return _Category.Value;
            }
        }

        private Lazy<ISiteImageRepository> _SiteImage;
        public ISiteImageRepository SiteImage
        {
            get
            {
                return _SiteImage.Value;
            }
        }

        private Lazy<IRecipeRepository> _Recipe;
        public IRecipeRepository Recipe
        {
            get
            {
                return _Recipe.Value;
            }
        }
        
        private Lazy<ICookbookRepository> _Cookbook;
        public ICookbookRepository Cookbook
        {
            get
            {
                return _Cookbook.Value;
            }
        }        
        private Lazy<ICommunityRepository> _Community;
        public ICommunityRepository Community
        {
            get
            {
                return _Community.Value;
            }
        }

        private Lazy<IStateRepository> _State;
        public IStateRepository State
        {
            get
            {
                return _State.Value;
            }
        }

        private Lazy<ICookbookCategoryRepository> _CookbookCategory;
        public ICookbookCategoryRepository CookbookCategory
        {
            get
            {
                return _CookbookCategory.Value;
            }
        }
        
        private Lazy<ICommunityCookbookRepository> _CommunityCookbook;
        public ICommunityCookbookRepository CommunityCookbook
        {
            get
            {
                return _CommunityCookbook.Value;
            }
        }
        
        private Lazy<ICountryRepository> _Country;
        public ICountryRepository Country
        {
            get
            {
                return _Country.Value;
            }
        }

        private Lazy<IApplicationUserRepository> _ApplicationUser;
        public IApplicationUserRepository ApplicationUser
        {
            get
            {
                return _ApplicationUser.Value;
            }
        }

        private Lazy<IPostalCodeRepository> _PostalCode;
        public IPostalCodeRepository PostalCode
        {
            get
            {
                return _PostalCode.Value;
            }
        }
        private Lazy<IGeocodeRepository> _Geocode;
        public IGeocodeRepository Geocode
        {
            get
            {
                return _Geocode.Value;
            }
        }

        private Lazy<IOrganizationRepository> _Organization;
        public IOrganizationRepository Organization
        {
            get
            {
                return _Organization.Value;
            }
        }

        private Lazy<IUserImageRepository> _UserImage;
        public IUserImageRepository UserImage
        {
            get
            {
                return _UserImage.Value;
            }
        }        
        private Lazy<IIngredientRepository> _Ingredient;
        public IIngredientRepository Ingredient
        {
            get
            {
                return _Ingredient.Value;
            }
        }        
        private Lazy<IInstructionRepository> _Instruction;
        public IInstructionRepository Instruction
        {
            get
            {
                return _Instruction.Value;
            }
        }        
        private Lazy<IWebReferenceRepository> _WebReference;
        public IWebReferenceRepository WebReference
        {
            get
            {
                return _WebReference.Value;
            }
        }

        private Lazy<ICommunityStateRepository> _CommunityState;
        public ICommunityStateRepository CommunityState
        {
            get
            {
                return _CommunityState.Value;
            }
        }        
        private Lazy<IOrganizationCommunityRepository> _OrganizationCommunity;
        public IOrganizationCommunityRepository OrganizationCommunity
        {
            get
            {
                return _OrganizationCommunity.Value;
            }
        }

        private Lazy<IOrganizationCookbookRepository> _OrganizationCookbook;
        public IOrganizationCookbookRepository OrganizationCookbook
        {
            get
            {
                return _OrganizationCookbook.Value;
            }
        }

        private Lazy<IRecipeCategoryRepository> _RecipeCategory;
        public IRecipeCategoryRepository RecipeCategory
        {
            get
            {
                return _RecipeCategory.Value;
            }
        }
        
        private Lazy<IRecipeIngredientRepository> _RecipeIngredient;
        public IRecipeIngredientRepository RecipeIngredient
        {
            get
            {
                return _RecipeIngredient.Value;
            }
        }
        
        private Lazy<IRecipeUserImageRepository> _RecipeUserImage;
        public IRecipeUserImageRepository RecipeUserImage
        {
            get
            {
                return _RecipeUserImage.Value;
            }
        }
        
        private Lazy<ICookbookRecipeRepository> _CookbookRecipe;
        public ICookbookRecipeRepository CookbookRecipe
        {
            get
            {
                return _CookbookRecipe.Value;
            }
        }

        private Lazy<IApplicationUserRecipeRepository> _ApplicationUserRecipe;
        public IApplicationUserRecipeRepository ApplicationUserRecipe
        {
            get
            {
                return _ApplicationUserRecipe.Value;
            }
        }
        
        private Lazy<IApplicationUserUserImageRepository> _ApplicationUserUserImage;
        public IApplicationUserUserImageRepository ApplicationUserUserImage
        {
            get
            {
                return _ApplicationUserUserImage.Value;
            }
        }
        
        private Lazy<IApplicationUserCookbookRepository> _ApplicationUserCookbook;
        public IApplicationUserCookbookRepository ApplicationUserCookbook
        {
            get
            {
                return _ApplicationUserCookbook.Value;
            }
        }        

        private Lazy<ICommunityGeocodeRepository> _CommunityGeocode;
        public ICommunityGeocodeRepository CommunityGeocode
        {
            get
            {
                return _CommunityGeocode.Value;
            }
        }        

        private Lazy<IPostalCodeGeocodeRepository> _PostalCodeGeocode;
        public IPostalCodeGeocodeRepository PostalCodeGeocode
        {
            get
            {
                return _PostalCodeGeocode.Value;
            }
        }
        
        private Lazy<ICommunityPostalCodeRepository> _CommunityPostalCode;
        public ICommunityPostalCodeRepository CommunityPostalCode
        {
            get
            {
                return _CommunityPostalCode.Value;
            }
        }

        private Lazy<ICommunityWebReferenceRepository> _CommunityWebReference;
        public ICommunityWebReferenceRepository CommunityWebReference
        {
            get
            {
                return _CommunityWebReference.Value;
            }
        }
        
        private Lazy<ICommunityRecipeRepository> _CommunityRecipe;
        public ICommunityRecipeRepository CommunityRecipe
        {
            get
            {
                return _CommunityRecipe.Value;
            }
        }        

        #endregion
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            #region model constructors
            //this._x = new Lazy<IXRepository>(() => new XRepository(this._db));
            this._Category = new Lazy<ICategoryRepository>(() => new CategoryRepository(_db));
            this._SiteImage = new Lazy<ISiteImageRepository>(() => new SiteImageRepository(this._db));
            this._Recipe = new Lazy<IRecipeRepository>(() => new RecipeRepository(this._db));
            this._Cookbook = new Lazy<ICookbookRepository>(() => new CookbookRepository(this._db));
            this._Community = new Lazy<ICommunityRepository>(() => new CommunityRepository(this._db));
            this._State = new Lazy<IStateRepository>(() => new StateRepository(this._db));
            this._Country = new Lazy<ICountryRepository>(() => new CountryRepository(this._db));
            this._ApplicationUser = new Lazy<IApplicationUserRepository>(() => new ApplicationUserRepository(this._db));
            this._PostalCode = new Lazy<IPostalCodeRepository>(() => new PostalCodeRepository(this._db));
            this._Geocode = new Lazy<IGeocodeRepository>(() => new GeocodeRepository(this._db));
            this._Organization = new Lazy<IOrganizationRepository>(() => new OrganizationRepository(this._db));
            this._Ingredient = new Lazy<IIngredientRepository>(() => new IngredientRepository(this._db));
            this._UserImage = new Lazy<IUserImageRepository>(() => new UserImageRepository(this._db));
            this._Instruction = new Lazy<IInstructionRepository>(() => new InstructionRepository(this._db));
            this._WebReference = new Lazy<IWebReferenceRepository>(() => new WebReferenceRepository(this._db));
            #endregion
            #region relationship constructors
            //this._x = new Lazy<IXRepository>(() => new XRepository(this._db));
            this._RecipeCategory = new Lazy<IRecipeCategoryRepository>(() => new RecipeCategoryRepository(this._db));
            this._CookbookRecipe = new Lazy<ICookbookRecipeRepository>(() => new CookbookRecipeRepository(this._db));
            this._CommunityGeocode = new Lazy<ICommunityGeocodeRepository>(() => new CommunityGeocodeRepository(this._db));
            this._PostalCodeGeocode = new Lazy<IPostalCodeGeocodeRepository>(() => new PostalCodeGeocodeRepository(this._db));
            this._CommunityPostalCode = new Lazy<ICommunityPostalCodeRepository>(() => new CommunityPostalCodeRepository(this._db));
            this._CommunityWebReference = new Lazy<ICommunityWebReferenceRepository>(() => new CommunityWebReferenceRepository(this._db));
            this._RecipeUserImage = new Lazy<IRecipeUserImageRepository>(() => new RecipeUserImageRepository(this._db));
            this._ApplicationUserUserImage = new Lazy<IApplicationUserUserImageRepository>(() => new ApplicationUserUserImageRepository(this._db));
            this._CommunityCookbook = new Lazy<ICommunityCookbookRepository>(() => new CommunityCookbookRepository(this._db));
            this._CookbookCategory = new Lazy<ICookbookCategoryRepository>(() => new CookbookCategoryRepository(this._db));
            this._CommunityState = new Lazy<ICommunityStateRepository>(() => new CommunityStateRepository(this._db));
            this._OrganizationCommunity = new Lazy<IOrganizationCommunityRepository>(() => new OrganizationCommunityRepository(this._db));
            this._OrganizationCookbook = new Lazy<IOrganizationCookbookRepository>(() => new OrganizationCookbookRepository(this._db));
            this._CommunityRecipe = new Lazy<ICommunityRecipeRepository>(() => new CommunityRecipeRepository(this._db));
            this._ApplicationUserCookbook = new Lazy<IApplicationUserCookbookRepository>(() => new ApplicationUserCookbookRepository(this._db));
            this._ApplicationUserRecipe = new Lazy<IApplicationUserRecipeRepository>(() => new ApplicationUserRecipeRepository(this._db));
            #endregion
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