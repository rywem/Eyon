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
                cookbookViewModel.Cookbook = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == id.GetValueOrDefault(), includeProperties: "CommunityCookbooks,CookbookCategories");
                if (cookbookViewModel.Cookbook == null)
                    return NotFound();
                if (cookbookViewModel.Cookbook.CookbookCategories != null)
                {
                    var categories = cookbookViewModel.Cookbook.CookbookCategories.Select(x => x.Category).ToList();
                    if (categories != null)
                        cookbookViewModel.Categories = categories;
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
                if (cookbookViewModel.Cookbook.Id == 0)
                    _unitOfWork.Cookbook.Add(cookbookViewModel.Cookbook);
                else
                    _unitOfWork.Cookbook.Update(cookbookViewModel.Cookbook);

                _unitOfWork.Save();
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