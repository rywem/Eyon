using Eyon.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {        
        ICategoryRepository Category { get; }
        ISiteImageRepository SiteImage { get; }
        ICookbookRepository Cookbook { get; }
        ICookbookCategoryRepository CookbookCategory { get; }
        ICommunityCookbookRepository CommunityCookbook { get; }
        ICommunityRepository Community { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        ICommunityStateRepository CommunityState { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IApplicationUserOrganizationRepository ApplicationUserOrganization { get; }
        IOrganizationRepository Organization { get; }
        IOrganizationCommunityRepository OrganizationCommunity { get; }
        IOrganizationCookbookRepository OrganizationCookbook { get; }       
        IRecipeRepository Recipe { get; }
        IRecipeCategoryRepository RecipeCategory { get; }
        IRecipeUserImageRepository RecipeUserImage { get; }
        IUserImageRepository UserImage { get; }
        IApplicationUserRecipeRepository ApplicationUserRecipe { get; }
        IApplicationUserUserImageRepository ApplicationUserUserImage { get; }
        IApplicationUserCookbookRepository ApplicationUserCookbook { get; }
        IIngredientRepository Ingredient { get; }
        IInstructionRepository Instruction { get; }
        ICookbookRecipeRepository CookbookRecipe { get; }
        ICommunityGeocodeRepository CommunityGeocode { get; }
        IPostalCodeGeocodeRepository PostalCodeGeocode { get; }
        IGeocodeRepository Geocode { get; }
        ICommunityPostalCodeRepository CommunityPostalCode { get; }
        ICommunityWebReferenceRepository CommunityWebReference { get; }
        IWebReferenceRepository WebReference { get; }
        ICommunityRecipeRepository CommunityRecipe { get;  }
        IPostalCodeRepository PostalCode { get; }


        IFeedRepository Feed { get; }
        ITopicRepository<ITopicItem> Topic { get; }
        IApplicationUserFeedRepository ApplicationUserFeed { get; }
        IFeedCategoryRepository FeedCategory { get; }
        IFeedCommunityRepository FeedCommunity { get; }
        IFeedCookbookRepository FeedCookbook { get; }
        IFeedCountryRepository FeedCountry { get; }
        IFeedOrganizationRepository FeedOrganization { get; }
        IFeedProfileRepository FeedProfile { get; }
        IFeedRecipeRepository FeedRecipe { get; }
        IFeedStateRepository FeedState { get; }
        IFeedTopicRepository FeedTopic { get; }


        IDatabaseTransaction BeginTransaction();
        void Save();
        Task<int> SaveAsync();
    }
}

