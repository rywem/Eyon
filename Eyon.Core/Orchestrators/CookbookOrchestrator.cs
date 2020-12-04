using System;
using System.Collections.Generic;
using System.Linq;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using Eyon.Core.Data.Repository;
using Eyon.Models.Relationship;
using Eyon.Core.DataCalls;
using System.Threading.Tasks;
using Eyon.Models;
using Eyon.Core.Security;
using Eyon.Core.DataCalls.IDataCall;
using Eyon.Core.Orchestrators.IOrchestrator;
using Eyon.Core.Security.ISecurity;

namespace Eyon.Core.Orchestrators
{
    public class CookbookOrchestrator : ICookbookOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedDataCall _feedDataCall;
        private readonly IFeedSecurity _feedSecurity;
        public CookbookOrchestrator( IUnitOfWork unitOfWork, IFeedDataCall feedDataCall, IFeedSecurity feedSecurity)
        {
            this._unitOfWork = unitOfWork;
            this._feedDataCall = feedDataCall;
            this._feedSecurity = feedSecurity;
        }
        public CookbookViewModel Get(long id)
        {
            CookbookViewModel cookbookViewModel = new CookbookViewModel();
            cookbookViewModel.Cookbook = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == id, includeProperties: "CommunityCookbook,CookbookCategory,CookbookCategory.Category");

            if (cookbookViewModel.Cookbook != null && cookbookViewModel.Cookbook.Id > 0)
            {
                if (cookbookViewModel.Cookbook.CookbookCategory != null && cookbookViewModel.Cookbook.CookbookCategory.Count > 0)                                    
                    cookbookViewModel.CategorySelector.AddListItems(cookbookViewModel.Cookbook.CookbookCategory.Select(x => x.Category).ToList());
                
                if (cookbookViewModel.Cookbook.CommunityCookbook != null)
                {
                    var communities = cookbookViewModel.Cookbook.CommunityCookbook.Select(x => x.Community).ToList();
                    if (communities != null)
                        cookbookViewModel.Community = communities;
                }
            }
            return cookbookViewModel;
        }
        
        public async Task AddTransactionAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddAsync(currentUserId, cookbookViewModel);
                    // Todo, add community
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task AddAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            if ( cookbookViewModel.Cookbook.Id != 0 ) //New cookbook
                throw new SafeException("Cookbook already exists.");
            
            _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Cookbook.AddOwnerRelationship(currentUserId, cookbookViewModel.Cookbook, new ApplicationUserCookbook());
            await _unitOfWork.SaveAsync();
            var topic = _unitOfWork.Topic.AddFromITopicItem(cookbookViewModel.Cookbook);
            
            if ( !string.IsNullOrEmpty(cookbookViewModel.CategorySelector.ItemIds) )
            {
                foreach ( var id in cookbookViewModel.CategorySelector.ParseItemIds() )
                {
                    var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

                    if ( categoryFromDb != null )
                    {
                        _unitOfWork.CookbookCategory.AddFromEntities(cookbookViewModel.Cookbook, categoryFromDb);
                        cookbookViewModel.CategorySelector.Items.Add(categoryFromDb);
                    }
                }
            }
            await _unitOfWork.SaveAsync();

            var feedItemViewModel = cookbookViewModel.ToFeedItemViewModel();
            feedItemViewModel.Topics.Add(topic);            
            await _feedSecurity.AddAsync(currentUserId, feedItemViewModel, false);
        }       

        public async Task UpdateTransactionAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await UpdateAsync(currentUserId, cookbookViewModel);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }

            }
        }

        public async Task UpdateAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            // TODO Refactor with SOLID in mind.

            var objFromDb = await _unitOfWork.Cookbook.GetFirstOrDefaultAsync(x => x.Id == cookbookViewModel.Cookbook.Id, includeProperties: "CommunityCookbook,CookbookCategory,FeedCookbook,FeedCookbook.Feed");
            if ( objFromDb == null || objFromDb.Id == 0 )
                throw new SafeException("Record not found in database");
            
            // Update Categories
            List<long> categoryIdList = new List<long>();

            if ( !string.IsNullOrEmpty(cookbookViewModel.CategorySelector.ItemIds) )
            {
                categoryIdList = cookbookViewModel.CategorySelector.ParseItemIds();
                foreach ( var id in categoryIdList )
                {
                    //if existing relation does not exist, add the relationship
                    if ( objFromDb.CookbookCategory == null || ( objFromDb.CookbookCategory != null && !objFromDb.CookbookCategory.Any(x => x.CategoryId == id) ) )
                    {
                        // Only add the relation if the category exists
                        var categoryFromDb = await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
                        if ( categoryFromDb != null )
                        {
                            _unitOfWork.CookbookCategory.AddFromEntities(objFromDb, categoryFromDb);                            
                            cookbookViewModel.CategorySelector.Items.Add(categoryFromDb);
                        }
                        else
                        {
                            throw new SafeException("An error occurred.", new Exception("Attempted to insert CookbookRecipe relation, but did not own cookbook or cookbook did not exist."));
                        }
                    }
                }
            }
            // Check to see if any categories were removed.
            if ( objFromDb.CookbookCategory != null )
            {
                // check to see if any categories have been removed.
                foreach ( var item in objFromDb.CookbookCategory )
                {
                    if ( !categoryIdList.Any(x => x == item.CategoryId) )
                    {
                        var feedCategoryFromDb = await _unitOfWork.FeedCategory.GetFirstOrDefaultAsync(x => x.FeedId == objFromDb.FeedCookbook.Feed.Id && x.CategoryId == item.CategoryId);                        
                        _unitOfWork.CookbookCategory.Remove(item);
                    }
                }
            }
            // TODO CookbookOrganization            
            
            _unitOfWork.Cookbook.UpdateIfOwner(currentUserId, cookbookViewModel.Cookbook);
            await _unitOfWork.SaveAsync();
            await _feedSecurity.UpdateAsync(currentUserId, cookbookViewModel.ToFeedItemViewModel(objFromDb.FeedCookbook.Feed));
        }

        public async Task DeleteTransactionAsync( string currentApplicationUserId, Cookbook objFromDb )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await DeleteAsync(currentApplicationUserId, objFromDb);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }

            }
        }

        public async Task DeleteAsync( string currentApplicationUserId, Cookbook objFromDb )
        {
            foreach ( var item in objFromDb.CookbookCategory )
            {
                _unitOfWork.CookbookCategory.Remove(item);
            }
            foreach ( var item in objFromDb.ApplicationUserOwner )
            {
                _unitOfWork.ApplicationUserCookbook.Remove(item);
            }

            foreach ( var item in objFromDb.CookbookRecipe )
            {
                _unitOfWork.CookbookRecipe.Remove(item);
            }

            foreach ( var item in objFromDb.OrganizationCookbook )
            {
                _unitOfWork.OrganizationCookbook.Remove(item);
            }

            foreach ( var item in objFromDb.CommunityCookbook )
            {
                _unitOfWork.CommunityCookbook.Remove(item);
            }

            if ( objFromDb.FeedCookbook != null )
            {                
                await _feedSecurity.DeleteAsync(currentApplicationUserId, objFromDb.FeedCookbook.FeedId, false);
            }
            // delete topic            
            _unitOfWork.Topic.RemoveFromITopicItem(objFromDb);

            await _unitOfWork.SaveAsync();
            _unitOfWork.Cookbook.Remove(objFromDb);
            await _unitOfWork.SaveAsync();
        }

        public void UpdateCookbook(string currentUserId, CookbookViewModel cookbookViewModel)
        {
            if ( _unitOfWork.Cookbook.IsOwner(currentUserId, cookbookViewModel.Cookbook.Id)  == false)
            {
                throw new SafeException("An error occurred.");
            }
            var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == cookbookViewModel.Cookbook.Id, includeProperties: "CommunityCookbook,CookbookCategory,FeedCookbook,FeedCookbook.Feed");
            if (objFromDb == null || objFromDb.Id == 0)
                throw new SafeException("Record not found in database");

            FeedDataCall feedCaller = new FeedDataCall(_unitOfWork);
            feedCaller.UpdateFeed(currentUserId, objFromDb.FeedCookbook.Feed, cookbookViewModel.Cookbook);
            // Update Categories
            List<long> categoryIdList = new List<long>();

            if ( !string.IsNullOrEmpty(cookbookViewModel.CategorySelector.ItemIds) )
            {
                categoryIdList = cookbookViewModel.CategorySelector.ParseItemIds();
                foreach ( var id in categoryIdList )
                {
                    //if existing relation does not exist, add the relationship
                    if ( objFromDb.CookbookCategory == null || ( objFromDb.CookbookCategory != null && !objFromDb.CookbookCategory.Any(x => x.CategoryId == id) ) )
                    {
                        // Only add the relation if the category exists
                        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
                        if ( categoryFromDb != null )
                        {
                            _unitOfWork.CookbookCategory.AddFromEntities(objFromDb, categoryFromDb);
                            _unitOfWork.FeedCategory.AddFromEntities(objFromDb.FeedCookbook.Feed, categoryFromDb);
                        }
                        else
                        {
                            throw new SafeException("An error occurred.", new Exception("Attempted to insert CookbookRecipe relation, but did not own cookbook or cookbook did not exist."));
                        }
                    }
                }
            }
            // Check to see if any categories were removed.
            if ( objFromDb.CookbookCategory != null )
            {
                // check to see if any categories have been removed.
                foreach ( var item in objFromDb.CookbookCategory )
                {
                    if ( !categoryIdList.Any(x => x == item.CategoryId) )
                    {
                        var feedCategoryFromDb = _unitOfWork.FeedCategory.GetFirstOrDefault(x => x.FeedId == objFromDb.FeedCookbook.Feed.Id && x.CategoryId == item.CategoryId);
                        if ( feedCategoryFromDb != null )
                            feedCaller.RemoveFeedCategory(feedCategoryFromDb);

                        _unitOfWork.CookbookCategory.Remove(item);
                    }
                }
            }           
            //Todo add other relationship updates
            _unitOfWork.Cookbook.UpdateIfOwner(currentUserId, cookbookViewModel.Cookbook);
            _unitOfWork.Save();
        }
    }
}
