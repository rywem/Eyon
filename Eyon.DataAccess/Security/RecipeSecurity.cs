using Eyon.DataAccess.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eyon.Utilities.Extensions;
using Eyon.Utilities.API;
using System.Linq;
using Eyon.DataAccess.Security.ISecurity;
using Eyon.DataAccess.Orchestrators.IOrchestrator;

namespace Eyon.DataAccess.Security
{
    public class RecipeSecurity : IRecipeSecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRecipeOrchestrator _recipeOrchestrator;
        private IFeedSecurity _feedSecurity;
        private IConfiguration _config;
        public RecipeSecurity( IUnitOfWork unitOfWork, IConfiguration config, IRecipeOrchestrator recipeOrchestrator, IFeedSecurity feedSecurity )
        {
            this._unitOfWork = unitOfWork;
            this._config = config;
            this._recipeOrchestrator = recipeOrchestrator;
            this._feedSecurity = feedSecurity;
        }

        public async Task<RecipeViewModel> GetAsync(string currentApplicationUserId, long id )
        {
            // check that the Recipe exists.
            if ( await _unitOfWork.Recipe.AnyAsync(x => x.Id == id) )
            {
                // check privacy, if private, only an owner can access.
                if ( await _unitOfWork.Recipe.UserCanViewAsync(currentApplicationUserId, id) )
                {
                    // determine if this is the current owner to render the correct UI components
                    var recipeViewModel = await this._recipeOrchestrator.GetAsync(currentApplicationUserId, id);

                    if ( recipeViewModel.UserImage != null && recipeViewModel.UserImage.Count > 0 )
                    {
                        using ( AmazonWebService service = new AmazonWebService(_config.GetValue<string>("AWS:AccessKey"), _config.GetValue<string>("AWS:SecretKey")) )
                        {
                            recipeViewModel.UserImage.GetImagesUrl(x => x.Image = service.GetPreSignedUrl(_config.GetValue<string>("AWS:Bucket"), x.FileName),
                                                                   x => x.Thumb = service.GetPreSignedUrl(_config.GetValue<string>("AWS:Bucket"), x.FileNameThumb)).ToList();
                        }
                    }
                    return recipeViewModel;
                }
                else
                    throw new SafeException(Models.Enums.ErrorType.Denied);
            }
            else
            {
                throw new SafeException(Models.Enums.ErrorType.NotFound);
            }
        }

        public async Task UpdateAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            // Ensure ownership of recipe record
            if ( await _unitOfWork.Recipe.IsOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe.Id) )
            {
                await _recipeOrchestrator.UpdateTransactionAsync(currentApplicationUserId, recipeViewModel);
            }
            else
            {
                throw new SafeException(Models.Enums.ErrorType.Denied, new Exception(string.Format("Attempted to update recipe, but was not the owner. Violating userId {0}, Attempted to update Recipe ID {1}", currentApplicationUserId, recipeViewModel.Recipe.Id)));                
            }
        }

        public async Task AddAsync(string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            await this._recipeOrchestrator.AddTransactionAsync(currentApplicationUserId, recipeViewModel);
        }

        public async Task DeleteAsync(string currentApplicationUserId, long id )
        {
            if (!await _unitOfWork.Recipe.IsOwnerAsync(currentApplicationUserId, id) )
            {
                throw new SafeException(Models.Enums.ErrorType.Denied, new Exception(string.Format("Owned item not found. Recipe ID {0},  Current application user ID {1}", id, currentApplicationUserId)));
            }            

            await this._recipeOrchestrator.DeleteTransactionAsync(currentApplicationUserId, id);            
        }
    }
}
