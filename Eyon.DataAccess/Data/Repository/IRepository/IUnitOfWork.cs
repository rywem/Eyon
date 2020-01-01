using System;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ISiteImageRepository SiteImage { get; }
        ICookbookRepository Cookbook { get; }
        ICookbookCategoriesRepository CookbookCategories { get; }
        ICommunityCookbookRepository CommunityCookbooks { get; }
        ICommunityRepository Community { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        ICommunityStateRepository CommunityState { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrganizationRepository Organization { get; }
        IOrganizationCommunitiesRepository OrganizationCommunities { get; }
        IOrganizationCookbooksRepository OrganizationCookbooks { get; }
        IApplicationUserRecipeRepository ApplicationUserRecipe { get; }


        IRecipeRepository Recipe { get; }
        IRecipeCategoryRepository RecipeCategory { get; }        
        IRecipeSiteImageRepository RecipeSiteImage { get; }

        IIngredientRepository Ingredient { get; }
        IInstructionRepository Instruction { get; }
        ICookbookRecipeRepository CookbookRecipe { get; }
        ICommunityGeocodeRepository CommunityGeocode { get; }
        IPostalCodeGeocodeRepository PostalCodeGeocode { get; }
        IGeocodeRepository Geocode { get; }
        ICommunityPostalCodeRepository CommunityPostalCode { get; }
        ICommunityWebReferenceRepository CommunityWebReference { get; }
        IWebReferenceRepository WebReference { get; }

        IPostalCodeRepository PostalCode { get; }

        IDatabaseTransaction BeginTransaction();
        void Save();
        Task<int> SaveAsync();
    }
}

