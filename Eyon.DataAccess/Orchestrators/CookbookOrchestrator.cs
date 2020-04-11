using System;
using System.Collections.Generic;
using System.Linq;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using Eyon.DataAccess.Data.Repository;
using Eyon.Models.Relationship;
using Eyon.DataAccess.DataCalls;
using System.Threading.Tasks;
using Eyon.Models;
using Eyon.DataAccess.Security;

namespace Eyon.DataAccess.Orchestrators
{
    public class CookbookOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        public CookbookOrchestrator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public CookbookViewModel GetCookbookViewModel(long id)
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

        /// <summary>
        /// Handles adding new cookbooks and their relationships
        /// </summary>
        /// <param name="cookbookViewModel">The CookbookViewModel </param>
        /// <returns>The cookbookviewmodel</returns>
        public void AddCookbook(string currentUserId, CookbookViewModel cookbookViewModel)
        {
            if (cookbookViewModel.Cookbook.Id != 0) //New cookbook
                throw new SafeException("Cookbook already exists.");

            FeedDataCall feedCaller = new FeedDataCall(_unitOfWork);
            _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
            _unitOfWork.Save();
            _unitOfWork.Cookbook.AddOwnerRelationship(currentUserId, cookbookViewModel.Cookbook, new ApplicationUserCookbook());
            _unitOfWork.Save();
            var topic = _unitOfWork.Topic.AddFromITopicItem(cookbookViewModel.Cookbook);
            var feed = _unitOfWork.Feed.AddFromIFeedItem(cookbookViewModel.Cookbook);
            _unitOfWork.Save();


            if ( !string.IsNullOrEmpty(cookbookViewModel.CategorySelector.ItemIds) )
            {
                foreach ( var id in cookbookViewModel.CategorySelector.ParseItemIds() )
                {
                    var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

                    if ( categoryFromDb != null )
                    {
                        _unitOfWork.CookbookCategory.AddFromEntities(cookbookViewModel.Cookbook, categoryFromDb);
                        _unitOfWork.FeedCategory.AddFromEntities(feed, categoryFromDb);
                    }
                }
            }
            //if (!string.IsNullOrEmpty(cookbookViewModel.CategoryIds))
            //{
            //    string[] categories = cookbookViewModel.CategoryIds.Split(',');

            //    if (categories.Length > 0)
            //    {

            //        for (int i = 0; i < categories.Length; i++)
            //        {
            //            long id = 0;
            //            if (long.TryParse(categories[i], out id) && id != 0)
            //            {
            //                _unitOfWork.CookbookCategory.Add(new Eyon.Models.Relationship.CookbookCategory()
            //                {
            //                    CategoryId = id,
            //                    CookbookId = cookbookViewModel.Cookbook.Id
            //                });
            //                _unitOfWork.Save();                            
            //            }
            //            else
            //            {
            //                throw new SafeException("Invalid category selected.");
            //            }
            //        }
            //    }
            //}
            // Todo, add community

        }

        public void AddCookbookTransaction(string currentUserId, CookbookViewModel cookbookViewModel)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    AddCookbook(currentUserId, cookbookViewModel);
                    // Todo, add community
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task AddTransactionAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddAsync(currentUserId, cookbookViewModel);
                    // Todo, add community
                    transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        private async Task AddAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            if ( cookbookViewModel.Cookbook.Id != 0 ) //New cookbook
                throw new SafeException("Cookbook already exists.");

            FeedDataCall feedCaller = new FeedDataCall(_unitOfWork);
            _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Cookbook.AddOwnerRelationship(currentUserId, cookbookViewModel.Cookbook, new ApplicationUserCookbook());
            await _unitOfWork.SaveAsync();
            var topic = _unitOfWork.Topic.AddFromITopicItem(cookbookViewModel.Cookbook);
            var feed = _unitOfWork.Feed.AddFromIFeedItem(cookbookViewModel.Cookbook);
            await _unitOfWork.SaveAsync();
            feedCaller.AddOwnerRelationship(currentUserId, feed);
            FeedCookbook feedCookbook = new FeedCookbook()
            {
                CookbookId = cookbookViewModel.Cookbook.Id,
                FeedId = feed.Id
            };

            _unitOfWork.FeedCookbook.Add(feedCookbook);
            _unitOfWork.FeedTopic.AddFromEntities(feed, topic);
            // todo delete extra feed items in database for cookbook   select * from feed


            if ( !string.IsNullOrEmpty(cookbookViewModel.CategorySelector.ItemIds) )
            {
                foreach ( var id in cookbookViewModel.CategorySelector.ParseItemIds() )
                {
                    var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

                    if ( categoryFromDb != null )
                    {
                        _unitOfWork.CookbookCategory.AddFromEntities(cookbookViewModel.Cookbook, categoryFromDb);
                        _unitOfWork.FeedCategory.AddFromEntities(feed, categoryFromDb);
                    }
                }
            }
            await _unitOfWork.SaveAsync();
        }

        public void UpdateCookbookTransaction(string currentUserId, CookbookViewModel cookbookViewModel)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    UpdateCookbook( currentUserId, cookbookViewModel);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
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

        private async Task UpdateAsync( string currentUserId, CookbookViewModel cookbookViewModel )
        {
            var objFromDb = await _unitOfWork.Cookbook.GetFirstOrDefaultAsync(x => x.Id == cookbookViewModel.Cookbook.Id, includeProperties: "CommunityCookbook,CookbookCategory,FeedCookbook,FeedCookbook.Feed");
            if ( objFromDb == null || objFromDb.Id == 0 )
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
                        var categoryFromDb = await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
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
                        var feedCategoryFromDb = await _unitOfWork.FeedCategory.GetFirstOrDefaultAsync(x => x.FeedId == objFromDb.FeedCookbook.Feed.Id && x.CategoryId == item.CategoryId);
                        if ( feedCategoryFromDb != null )
                            feedCaller.RemoveFeedCategory(feedCategoryFromDb);

                        _unitOfWork.CookbookCategory.Remove(item);
                    }
                }
            }
            // TODO CookbookOrganization
            // 
            
            _unitOfWork.Cookbook.UpdateIfOwner(currentUserId, cookbookViewModel.Cookbook);
            await _unitOfWork.SaveAsync();
        }

        internal async Task DeleteTransactionAsync( string currentApplicationUserId, Cookbook objFromDb )
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

        private async Task DeleteAsync( string currentApplicationUserId, Cookbook objFromDb )
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
                FeedSecurity feedSecurity = new FeedSecurity(_unitOfWork);
                await feedSecurity.DeleteAsync(currentApplicationUserId, objFromDb.FeedCookbook.FeedId, false);
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
            //if (!string.IsNullOrEmpty(cookbookViewModel.CategoryIds))
            //{
            //    string[] categories = cookbookViewModel.CategoryIds.Split(',');

            //    if (categories.Length > 0)
            //    {
            //        List<long> newCategories = new List<long>();
            //        for (int i = 0; i < categories.Length; i++)
            //        {
            //            long id = 0;
            //            if (long.TryParse(categories[i], out id))
            //            {
            //                newCategories.Add(id);
            //            }
            //            else
            //            {
            //                throw new SafeException("Invalid category selected.");
            //            }
            //        }

            //        // find existing categories to remove
            //        foreach (var item in objFromDb.CookbookCategory)
            //        {
            //            if (newCategories.Contains(item.CategoryId))
            //                continue;
            //            else
            //                _unitOfWork.CookbookCategory.Remove(item);
            //        }
            //        _unitOfWork.Save();

            //        //Find new categories to add
            //        foreach (var item in newCategories)
            //        {
            //            var exist = objFromDb.CookbookCategory.FirstOrDefault(x => x.CategoryId == item);
            //            if (exist == null)
            //            {
            //                _unitOfWork.CookbookCategory.Add(new Eyon.Models.Relationship.CookbookCategory()
            //                {
            //                    CategoryId = item,
            //                    CookbookId = objFromDb.Id
            //                });
            //            }
            //        }
            //        _unitOfWork.Save();
            //    }
            //}
            //Todo add other relationship updates
            _unitOfWork.Cookbook.UpdateIfOwner(currentUserId, cookbookViewModel.Cookbook);
            _unitOfWork.Save();
        }
    }
}
