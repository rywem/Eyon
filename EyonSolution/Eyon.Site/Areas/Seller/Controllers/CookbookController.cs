using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class CookbookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CookbookViewModel cookbookViewModel { get; set; }
        public CookbookController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(long? id)
        {
            cookbookViewModel = new CookbookViewModel();
            if (id == null)
                return View(cookbookViewModel);

            if (id != null)
            {
                cookbookViewModel.Cookbook = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == id.GetValueOrDefault(), includeProperties: "CommunityCookbooks,CookbookCategories,CookbookCategories.Category");
                if (cookbookViewModel.Cookbook == null)
                    return NotFound();
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
            return View(cookbookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string[] categories = cookbookViewModel.CategoryIds.Split(',');
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        if (cookbookViewModel.Cookbook.Id == 0) //New cookbook
                        {
                            _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
                            _unitOfWork.Save();
                            for (int i = 0; i < categories.Length; i++)
                            {
                                long id = 0;
                                if (long.TryParse(categories[i], out id))
                                {
                                    _unitOfWork.CookbookCategories.Add(new Eyon.Models.Relationship.CookbookCategories()
                                    {
                                        CategoryId = id,
                                        CookbookId = cookbookViewModel.Cookbook.Id
                                    });
                                    _unitOfWork.Save();
                                }
                            }                            
                        }
                        else
                        {
                            var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == cookbookViewModel.Cookbook.Id, includeProperties: "CommunityCookbooks,CookbookCategories,CookbookCategories.Category");
                            
                            List<long> newCategories = new List<long>();
                            for (int i = 0; i < categories.Length; i++)
                            {
                                long id = 0;
                                if (long.TryParse(categories[i], out id))
                                {
                                    newCategories.Add(id);
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
                            foreach(var item in newCategories)
                            {
                                var exist = objFromDb.CookbookCategories.FirstOrDefault(x => x.CategoryId == item);
                                if( exist == null )
                                {
                                    _unitOfWork.CookbookCategories.Add(new Eyon.Models.Relationship.CookbookCategories()
                                    {
                                        CategoryId = item,
                                        CookbookId = objFromDb.Id
                                    });
                                }
                            }
                            _unitOfWork.Save();
                            _unitOfWork.Cookbook.Update(cookbookViewModel.Cookbook);
                            _unitOfWork.Save();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cookbookViewModel.Cookbook);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Cookbook.Get(id);

            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting, Id does not exist. " });

            _unitOfWork.Cookbook.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful." });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Cookbook.GetAll() });
        }
    }
}