﻿using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Eyon.Models;
using Eyon.DataAccess.Data.Repository;
using System.Threading.Tasks;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models.SiteObjects;
using Eyon.DataAccess.DataCalls;
using Eyon.DataAccess.Data.Abstraction;
using Eyon.DataAccess.Security;
using Microsoft.Extensions.Configuration;
using Eyon.DataAccess.Images;
using Eyon.DataAccess.Orchestrators.IOrchestrator;
using Eyon.DataAccess.DataCalls.IDataCall;
using Eyon.DataAccess.Security.ISecurity;

namespace Eyon.DataAccess.Orchestrators
{
    public class RecipeOrchestrator : IRecipeOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private IRecipeDataCall _recipeDataCall;
        private IFeedSecurity _feedSecurity;
        public RecipeOrchestrator( IUnitOfWork unitOfWork, IConfiguration config, IRecipeDataCall recipeDataCall, IFeedSecurity feedSecurity )
        {
            this._unitOfWork = unitOfWork;
            this._config = config;
            this._recipeDataCall = recipeDataCall;
            this._feedSecurity = feedSecurity;
        }

        public async Task<RecipeViewModel> GetAsync(string currentApplicationUserId, long id)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();            
            recipeViewModel.Recipe = await _unitOfWork.Recipe.GetFirstOrDefaultAsync( x => x.Id == id,
                includeProperties: "CommunityRecipe,CommunityRecipe,Instruction,Ingredient,CookbookRecipe,CookbookRecipe.Cookbook,RecipeUserImage,RecipeUserImage.UserImage,FeedRecipe,FeedRecipe.Feed,RecipeCategory,RecipeCategory.Category", false); 

            if ( recipeViewModel.Recipe != null )
            {
                
                recipeViewModel.IsOwner = await _unitOfWork.Recipe.IsOwnerAsync(currentApplicationUserId, recipeViewModel.Recipe.Id);

                if ( recipeViewModel.Recipe.CommunityRecipe != null )
                {
                    recipeViewModel.Community = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.CommunityRecipe.CommunityId, includeProperties: "CommunityState,CommunityState.State,Country");
                    recipeViewModel.CommunityName = Eyon.Models.Helpers.CommunityHelper.FullNameFormatter(recipeViewModel.Community);
                    recipeViewModel.CommunityId = recipeViewModel.Community.Id;
                }
                if ( recipeViewModel.Recipe.Instruction != null && recipeViewModel.Recipe.Instruction.Count > 0 )
                { 
                    recipeViewModel.Instruction = recipeViewModel.Recipe.Instruction.ToList();                    
                    recipeViewModel.InstructionText = string.Join(Environment.NewLine, recipeViewModel.Instruction.OrderBy(x => x.Count).Select(x => x.Text));                    
                }

                if ( recipeViewModel.Recipe.Ingredient != null && recipeViewModel.Recipe.Ingredient.Count > 0 )
                {
                    recipeViewModel.Ingredient = recipeViewModel.Recipe.Ingredient.ToList();
                    recipeViewModel.IngredientText = string.Join(Environment.NewLine, recipeViewModel.Recipe.Ingredient.ToList().Select(x => x.Text));
                }

                if ( recipeViewModel.Recipe.RecipeUserImage != null && recipeViewModel.Recipe.RecipeUserImage.Count > 0 )
                    recipeViewModel.UserImage = recipeViewModel.Recipe.RecipeUserImage.Select(x => x.UserImage).ToList();

                if ( recipeViewModel.Recipe.CookbookRecipe != null && recipeViewModel.Recipe.CookbookRecipe.Count > 0 )                
                    recipeViewModel.CookbookSelector.AddListItems(recipeViewModel.Recipe.CookbookRecipe.Select(x => x.Cookbook).ToList());

                if ( recipeViewModel.Recipe.RecipeCategory != null && recipeViewModel.Recipe.RecipeCategory.Count > 0 )
                    recipeViewModel.CategorySelector.AddListItems(recipeViewModel.Recipe.RecipeCategory.Select(x => x.Category).ToList());
            }
            return recipeViewModel;
        }

        public async Task AddTransactionAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddAsync(currentApplicationUserId, recipeViewModel);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task AddAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            var communityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.CommunityId);

            if ( communityFromDb == null )
                throw new SafeException("An error occurred");

            //FeedDataCall feedCaller = new FeedDataCall(_unitOfWork);

            recipeViewModel.Recipe = await _recipeDataCall.AddRecipeWithRelationship(currentApplicationUserId, recipeViewModel.Recipe, false);            
            var topic  = _unitOfWork.Topic.AddFromITopicItem(recipeViewModel.Recipe);
            await _unitOfWork.SaveAsync();
            _unitOfWork.CommunityRecipe.AddFromEntities(communityFromDb, recipeViewModel.Recipe);
            
            // Add instructions
            foreach ( var item in recipeViewModel.ParseInstructions() )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Instruction.Add(item);
            }            

            foreach ( var item in recipeViewModel.ParseIngredients() )
            {
                item.RecipeId = recipeViewModel.Recipe.Id;
                _unitOfWork.Ingredient.Add(item);
            }

            if (!string.IsNullOrEmpty(recipeViewModel.CookbookSelector.ItemIds))
            {
                foreach (var id in recipeViewModel.CookbookSelector.ParseItemIds())
                {
                    // Only add the relation if the current user owns the cookbook id
                    var cookbookFromDb = await _unitOfWork.Cookbook.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == id);

                    if (cookbookFromDb != null)
                    {
                        _unitOfWork.CookbookRecipe.AddFromEntities(cookbookFromDb, recipeViewModel.Recipe);
                        recipeViewModel.CookbookSelector.Items.Add(cookbookFromDb);
                    }
                    else
                    {
                        throw new SafeException(Models.Enums.ErrorType.Denied, new Exception("Attempted to insert CookbookRecipe relation, but did not own cookbook or cookbook did not exist."));
                    }
                }
                await _unitOfWork.SaveAsync();
            }
            
            if ( !string.IsNullOrEmpty(recipeViewModel.CategorySelector.ItemIds) )
            {                
                foreach ( var id in recipeViewModel.CategorySelector.ParseItemIds() )
                {
                    var categoryFromDb = await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);

                    if ( categoryFromDb != null )
                    {
                        _unitOfWork.RecipeCategory.AddFromEntities(recipeViewModel.Recipe, categoryFromDb);
                        //_unitOfWork.FeedCategory.AddFromEntities(feed, categoryFromDb);
                        recipeViewModel.CategorySelector.Items.Add(categoryFromDb);
                    }
                }
            }
            await _unitOfWork.SaveAsync();
            await AddRecipeUserImageAsync(currentApplicationUserId, recipeViewModel);

            var feedItemViewModel = recipeViewModel.ToFeedItemViewModel();
            feedItemViewModel.Topics.Add(topic);
            feedItemViewModel.Communities.Add(communityFromDb);
            await _feedSecurity.AddAsync(currentApplicationUserId, feedItemViewModel, false);
        }


        /// <summary>
        /// Users do not need to own the recipe to add the recipe image
        /// </summary>
        /// <param name="recipeViewModel"></param>
        /// <returns></returns>
        private async Task AddRecipeUserImageAsync(string currentApplicationUserId, RecipeViewModel recipeViewModel)
        {

            if ( recipeViewModel.UserImage != null && recipeViewModel.UserImage.Count > 0 )
            {
                var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.Recipe.Id);
                var applicationUserFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(x => x.Id == currentApplicationUserId);
                if ( recipeFromDb != null && applicationUserFromDb != null )
                {
                    foreach ( var item in recipeViewModel.UserImage )
                    {
                        if ( item.Id == 0 )
                            _unitOfWork.UserImage.Add(item);
                    }
                    await _unitOfWork.SaveAsync();

                    foreach ( var item in recipeViewModel.UserImage )
                    {
                        _unitOfWork.RecipeUserImage.Add(new RecipeUserImage()
                        {
                            RecipeId = recipeFromDb.Id,
                            UserImageId = item.Id
                        });
                        _unitOfWork.UserImage.AddOwnerRelationship(currentApplicationUserId, item, new ApplicationUserUserImage());                        
                    }
                    await _unitOfWork.SaveAsync();
                }
            }
        }
        public async Task UpdateTransactionAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await UpdateAsync(currentApplicationUserId, recipeViewModel);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task UpdateAsync( string currentApplicationUserId, RecipeViewModel recipeViewModel )
        {
            var recipeFromDb = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == recipeViewModel.Recipe.Id, includeProperties: "CommunityRecipe,CommunityRecipe.Community,Instruction,Ingredient,CookbookRecipe,FeedRecipe,FeedRecipe.Feed,RecipeCategory");
            if ( recipeFromDb == null )
            {
                return;
            }
            _unitOfWork.Recipe.UpdateIfOwner(currentApplicationUserId, recipeViewModel.Recipe);
            await _unitOfWork.SaveAsync();
            // Update Feed
            //FeedDataCall feedCaller = new FeedDataCall(_unitOfWork);
            //feedCaller.UpdateFeed(currentApplicationUserId, recipeFromDb.FeedRecipe.Feed, recipeViewModel.Recipe);            
            // instructions
            // remove instructions1
            recipeViewModel.ParseInstructions();
            if ( recipeViewModel.Instruction.Count < recipeFromDb.Instruction.Count )
            {
                var dbInstructionsList = recipeFromDb.Instruction.ToList();
                for ( int i = recipeViewModel.Instruction.Count; i < recipeFromDb.Instruction.Count; i++ )
                {
                    var itemToRemove = dbInstructionsList[i];
                    _unitOfWork.Instruction.Remove(itemToRemove.Id);
                }
            }
            // add or update new instructions
            foreach ( var item in recipeViewModel.Instruction )
            {
                var instructionFromDb = recipeFromDb.Instruction.FirstOrDefault(x => x.Count == item.Count);

                if(instructionFromDb == null ) // add
                {
                    item.RecipeId = recipeFromDb.Id;
                    _unitOfWork.Instruction.Add(item);                    
                }
                else
                {
                    if ( !instructionFromDb.Text.Equals(item.Text) )
                    {
                        instructionFromDb.Text = item.Text;
                        _unitOfWork.Instruction.Update(instructionFromDb);
                    }
                }
            }

            // ingredients
            recipeViewModel.ParseIngredients();
            // remove ingredients 
            if ( recipeViewModel.Ingredient.Count < recipeFromDb.Ingredient.Count )
            {
                var dbIngredientsList = recipeFromDb.Ingredient.ToList();
                
                for ( int i = recipeViewModel.Ingredient.Count; i < recipeFromDb.Ingredient.Count; i++ )
                {
                    var itemToRemove = dbIngredientsList[i];
                    _unitOfWork.Ingredient.Remove(itemToRemove.Id);                    
                }
            }
            int ingredientCounter = 0;

            // add or update ingredients            
            foreach ( var item in recipeViewModel.Ingredient )
            {
                var ingredientFromDb = recipeFromDb.Ingredient.FirstOrDefault(x => x.Count == item.Count );
                if ( ingredientFromDb == null )
                {
                    item.RecipeId = recipeFromDb.Id;
                    _unitOfWork.Ingredient.Add(item);
                }
                else
                {
                    if ( !ingredientFromDb.Text.Equals(item.Text) )
                    {
                        ingredientFromDb.Text = item.Text;
                        _unitOfWork.Ingredient.Update(ingredientFromDb);
                    }
                }
                ingredientCounter++;
            }

            await _unitOfWork.SaveAsync();

            
            // Update Community
            if ( recipeFromDb.CommunityRecipe == null && recipeViewModel.CommunityId > 0 )
            {
                var newCommunityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.CommunityId);
                var communityRecipe = new CommunityRecipe()
                {
                    CommunityId = newCommunityFromDb.Id,
                    RecipeId = recipeFromDb.Id
                };
                _unitOfWork.CommunityRecipe.Add(communityRecipe);
                recipeViewModel.Community = newCommunityFromDb;
                await _unitOfWork.SaveAsync();
            }
            else if ( recipeFromDb.CommunityRecipe != null && recipeViewModel.CommunityId != recipeFromDb.CommunityRecipe.CommunityId )
            {
                var newCommunityFromDb = await _unitOfWork.Community.GetFirstOrDefaultAsync(x => x.Id == recipeViewModel.CommunityId);
                if ( newCommunityFromDb == null )
                    throw new SafeException("An error occurred");

                _unitOfWork.CommunityRecipe.Remove(recipeFromDb.CommunityRecipe);                
                _unitOfWork.CommunityRecipe.Add(new CommunityRecipe() { RecipeId = recipeFromDb.Id, CommunityId = newCommunityFromDb.Id });                
                recipeViewModel.Community = newCommunityFromDb;
                await _unitOfWork.SaveAsync();
            }
            // Update cookbooks
            // add cookbooks/remove cookbooks
            List<long> cookbookIdList = new List<long>();
            if (!string.IsNullOrEmpty(recipeViewModel.CookbookSelector.ItemIds))
            {
                cookbookIdList = recipeViewModel.CookbookSelector.ParseItemIds();
                foreach (var id in cookbookIdList)
                {
                    //if existing relation does not exist, add the relationship
                    if ( recipeFromDb.CookbookRecipe == null || (recipeFromDb.CookbookRecipe != null && !recipeFromDb.CookbookRecipe.Any(x => x.CookbookId == id)))
                    {
                        // Only add the relation if the current user owns the target cookbook and the cookbook exists.
                        var cookbookFromDb = await _unitOfWork.Cookbook.GetFirstOrDefaultOwnedAsync(currentApplicationUserId, x => x.Id == id);
                        if (cookbookFromDb != null)
                        {
                            _unitOfWork.CookbookRecipe.AddFromEntities(cookbookFromDb, recipeViewModel.Recipe);
                            recipeViewModel.CookbookSelector.Items.Add(cookbookFromDb); // fill out the recipeViewModel so that the feedItemViewModel will have accurate data.
                            //_unitOfWork.FeedCookbook.AddFromEntities(recipeFromDb.FeedRecipe.Feed, cookbookFromDb);
                        }
                        else
                        {
                            throw new SafeException("An error occurred.", new Exception("Attempted to insert CookbookRecipe relation, but did not own cookbook or cookbook did not exist."));
                        }
                    }
                }
            }
            if (recipeFromDb.CookbookRecipe != null )
            {
                // check to see if any cookbooks have been removed.
                foreach (var item in recipeFromDb.CookbookRecipe)
                {
                    if (!cookbookIdList.Any(x => x == item.CookbookId))
                    {
                        _unitOfWork.CookbookRecipe.Remove(item);
                    }
                }
            }


            // Update Categories
            List<long> categoryIdList = new List<long>();

            if ( !string.IsNullOrEmpty(recipeViewModel.CategorySelector.ItemIds) )
            {
                categoryIdList = recipeViewModel.CategorySelector.ParseItemIds();
                foreach ( var id in categoryIdList )
                {
                    //if existing relation does not exist, add the relationship
                    if ( recipeFromDb.RecipeCategory == null || (recipeFromDb.RecipeCategory != null && !recipeFromDb.RecipeCategory.Any(x => x.CategoryId == id)) )
                    {
                        // Only add the relation if the category exists
                        var categoryFromDb = await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
                        if ( categoryFromDb != null )
                        {                            
                            _unitOfWork.RecipeCategory.AddFromEntities(recipeFromDb, categoryFromDb);                            
                            recipeViewModel.CategorySelector.Items.Add(categoryFromDb);
                        }
                        else
                        {
                            throw new SafeException("An error occurred.", new Exception("Attempted to insert CookbookRecipe relation, but did not own cookbook or cookbook did not exist."));
                        }
                    }
                }
            }
            // Check to see if any categories were removed.
            if ( recipeFromDb.RecipeCategory != null )
            {
                // check to see if any categories have been removed.
                foreach ( var item in recipeFromDb.RecipeCategory )
                {
                    if ( !categoryIdList.Any(x => x == item.CategoryId) )
                    {
                        _unitOfWork.RecipeCategory.Remove(item);
                    }
                }
            }

            await _unitOfWork.SaveAsync();

            if ( recipeFromDb.FeedRecipe != null && recipeFromDb.FeedRecipe.Feed != null)
                await _feedSecurity.UpdateAsync(currentApplicationUserId, recipeViewModel.ToFeedItemViewModel(recipeFromDb.FeedRecipe.Feed), false);
        }

        public async Task DeleteTransactionAsync( string currentApplicationUserId, Recipe recipe )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await DeleteAsync( currentApplicationUserId, recipe);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task DeleteAsync( string currentApplicationUserId, Recipe recipe )
        {
            if ( recipe.CommunityRecipe != null )
            {
                _unitOfWork.CommunityRecipe.Remove(recipe.CommunityRecipe);
            }

            if ( recipe.Instruction != null )
            {
                foreach ( var item in recipe.Instruction )
                {
                    _unitOfWork.Instruction.Remove(item);
                }
            }
            if ( recipe.Ingredient != null )
            {
                foreach ( var item in recipe.Ingredient )
                {
                    _unitOfWork.Ingredient.Remove(item);
                }
            }
            if ( recipe.RecipeUserImage != null )
            {
                List<Task> tasks = new List<Task>();
                ImageHelper helper = new ImageHelper(_config);
                foreach ( var item in recipe.RecipeUserImage )
                {
                    tasks.Add(helper.TryDeleteAsync(item.UserImage.FileName));
                    tasks.Add(helper.TryDeleteAsync(item.UserImage.FileNameThumb));
                    _unitOfWork.UserImage.Remove(item.UserImage);
                    _unitOfWork.RecipeUserImage.Remove(item);
                }
                await Task.WhenAll(tasks);
            }

            if ( recipe.RecipeCategory != null )
            {
                foreach ( var item in recipe.RecipeCategory )
                {
                    _unitOfWork.RecipeCategory.Remove(item);
                }
            }

            if ( recipe.CookbookRecipe != null )
            {
                foreach ( var item in recipe.CookbookRecipe )
                {
                    _unitOfWork.CookbookRecipe.Remove(item);
                }
            }
            
            var topic = await _unitOfWork.Topic.GetFirstOrDefaultAsync(x => x.ObjectId == recipe.Id && x.TopicType == recipe.TopicType);
            if ( topic != null )
                _unitOfWork.Topic.Remove(topic);

            await _unitOfWork.SaveAsync();
            if ( recipe.FeedRecipe != null )
            {
                await _feedSecurity.DeleteAsync(currentApplicationUserId, recipe.FeedRecipe.FeedId, false);
            }

            if ( recipe.ApplicationUserOwner != null )
            {
                foreach ( var item in recipe.ApplicationUserOwner )
                {
                    _unitOfWork.ApplicationUserRecipe.Remove(item);
                }
            }

            _unitOfWork.Recipe.Remove(recipe);
            await _unitOfWork.SaveAsync();
        }
    }
}
