using System;
using System.Collections.Generic;
using System.Linq;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;
using Eyon.DataAccess.Data.Repository;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data.Orchestrators
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
                if (cookbookViewModel.Cookbook.CookbookCategory != null)
                {
                    var categories = cookbookViewModel.Cookbook.CookbookCategory.Select(x => x.Category).ToList();
                    if (categories != null)
                        cookbookViewModel.Category = categories;

                    cookbookViewModel.SetCategoryIds();
                    cookbookViewModel.SetCategoryNames();
                }
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
            _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
            _unitOfWork.Save();
            _unitOfWork.Cookbook.AddOwnerRelationship(currentUserId, cookbookViewModel.Cookbook, new ApplicationUserCookbook());
            _unitOfWork.Save();
            if (!string.IsNullOrEmpty(cookbookViewModel.CategoryIds))
            {
                string[] categories = cookbookViewModel.CategoryIds.Split(',');

                if (categories.Length > 0)
                {

                    for (int i = 0; i < categories.Length; i++)
                    {
                        long id = 0;
                        if (long.TryParse(categories[i], out id) && id != 0)
                        {
                            _unitOfWork.CookbookCategory.Add(new Eyon.Models.Relationship.CookbookCategories()
                            {
                                CategoryId = id,
                                CookbookId = cookbookViewModel.Cookbook.Id
                            });
                            _unitOfWork.Save();                            
                        }
                        else
                        {
                            throw new SafeException("Invalid category selected.");
                        }
                    }
                }
            }
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


        public void UpdateCookbook(string currentUserId, CookbookViewModel cookbookViewModel)
        {
            if ( _unitOfWork.Cookbook.IsOwner(currentUserId, cookbookViewModel.Cookbook.Id)  == false)
            {
                throw new SafeException("An error occurred.");
            }
            var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == cookbookViewModel.Cookbook.Id, includeProperties: "CommunityCookbook,CookbookCategory");
            if (objFromDb == null || objFromDb.Id == 0)
                throw new SafeException("Record not found in database");

            if (!string.IsNullOrEmpty(cookbookViewModel.CategoryIds))
            {
                string[] categories = cookbookViewModel.CategoryIds.Split(',');

                if (categories.Length > 0)
                {
                    List<long> newCategories = new List<long>();
                    for (int i = 0; i < categories.Length; i++)
                    {
                        long id = 0;
                        if (long.TryParse(categories[i], out id))
                        {
                            newCategories.Add(id);
                        }
                        else
                        {
                            throw new SafeException("Invalid category selected.");
                        }
                    }

                    // find existing categories to remove
                    foreach (var item in objFromDb.CookbookCategory)
                    {
                        if (newCategories.Contains(item.CategoryId))
                            continue;
                        else
                            _unitOfWork.CookbookCategory.Remove(item);
                    }
                    _unitOfWork.Save();

                    //Find new categories to add
                    foreach (var item in newCategories)
                    {
                        var exist = objFromDb.CookbookCategory.FirstOrDefault(x => x.CategoryId == item);
                        if (exist == null)
                        {
                            _unitOfWork.CookbookCategory.Add(new Eyon.Models.Relationship.CookbookCategories()
                            {
                                CategoryId = item,
                                CookbookId = objFromDb.Id
                            });
                        }
                    }
                    _unitOfWork.Save();
                }
            }
            //Todo add other relationship updates
            _unitOfWork.Cookbook.UpdateIfOwner(currentUserId, cookbookViewModel.Cookbook);
            _unitOfWork.Save();
        }
    }
}
