using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Errors;
using Eyon.Models.ViewModels;

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
            cookbookViewModel.Cookbook = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == id, includeProperties: "CommunityCookbooks,CookbookCategories,CookbookCategories.Category");

            if (cookbookViewModel.Cookbook != null && cookbookViewModel.Cookbook.Id > 0)
            {
                if (cookbookViewModel.Cookbook.CookbookCategories != null)
                {
                    var categories = cookbookViewModel.Cookbook.CookbookCategories.Select(x => x.Category).ToList();
                    if (categories != null)
                        cookbookViewModel.Categories = categories;

                    cookbookViewModel.SetCategoryIds();
                    cookbookViewModel.SetCategoryNames();
                }
                if (cookbookViewModel.Cookbook.CommunityCookbooks != null)
                {
                    var communities = cookbookViewModel.Cookbook.CommunityCookbooks.Select(x => x.Community).ToList();
                    if (communities != null)
                        cookbookViewModel.Communities = communities;
                }
            }
            return cookbookViewModel;
        }

        /// <summary>
        /// Handles adding new cookbooks and their relationships
        /// </summary>
        /// <param name="cookbookViewModel">The CookbookViewModel </param>
        /// <returns>The cookbookviewmodel</returns>
        public void AddCookbook(CookbookViewModel cookbookViewModel)
        {
            if (cookbookViewModel.Cookbook.Id != 0) //New cookbook
                throw new WebUserSafeException("Cookbook already exists.");
            _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
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
                            _unitOfWork.CookbookCategories.Add(new Eyon.Models.Relationship.CookbookCategories()
                            {
                                CategoryId = id,
                                CookbookId = cookbookViewModel.Cookbook.Id
                            });
                            _unitOfWork.Save();
                        }
                        else
                        {
                            throw new WebUserSafeException("Invalid category selected.");
                        }
                    }
                }
            }
            // Todo, add community

        }

        public void AddCookbookTransaction(CookbookViewModel cookbookViewModel)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    AddCookbook(cookbookViewModel);
                    // Todo, add community
                    transaction.Commit();
                }
                catch (Exception safeException)
                {
                    transaction.Rollback();
                    throw safeException;
                }
            }
        }

        public void UpdateCookbookTransaction(CookbookViewModel cookbookViewModel)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    UpdateCookbookTransaction(cookbookViewModel);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
        }


        public void UpdateCookbook(CookbookViewModel cookbookViewModel)
        {
            var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == cookbookViewModel.Cookbook.Id, includeProperties: "CommunityCookbooks,CookbookCategories");
            if (objFromDb == null || objFromDb.Id == 0)
                throw new WebUserSafeException("Record not found in database");

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
                            throw new WebUserSafeException("Invalid category selected.");
                        }
                    }

                    // find existing categories to remove
                    foreach (var item in objFromDb.CookbookCategories)
                    {
                        if (newCategories.Contains(item.CategoryId))
                            continue;
                        else
                            _unitOfWork.CookbookCategories.Remove(item);
                    }
                    _unitOfWork.Save();

                    //Find new categories to add
                    foreach (var item in newCategories)
                    {
                        var exist = objFromDb.CookbookCategories.FirstOrDefault(x => x.CategoryId == item);
                        if (exist == null)
                        {
                            _unitOfWork.CookbookCategories.Add(new Eyon.Models.Relationship.CookbookCategories()
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
            _unitOfWork.Cookbook.Update(cookbookViewModel.Cookbook);
            _unitOfWork.Save();
        }
    }
}
