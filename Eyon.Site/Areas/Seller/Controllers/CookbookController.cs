using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Eyon.DataAccess.Data.Orchestrators;
using Microsoft.AspNetCore.Authorization;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Authorize(Roles = "Admin,Manager,Seller")]
    [Area("Seller")]
    public class CookbookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private CookbookOrchestrator cookbookOrchestrator;
        [BindProperty]
        public CookbookViewModel cookbookViewModel { get; set; }
        public CookbookController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.cookbookOrchestrator = new CookbookOrchestrator(_unitOfWork);
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
                cookbookViewModel = cookbookOrchestrator.GetCookbookViewModel(id.GetValueOrDefault());
            }
            return View(cookbookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                //todo validate the user submitting has permission to add or edit this cookbook.
                try
                {
                    if (cookbookViewModel.Cookbook.Id == 0) //New cookbook
                    {
                        cookbookOrchestrator.AddCookbookTransaction(cookbookViewModel);
                    }
                    else
                    {
                        cookbookOrchestrator.UpdateCookbookTransaction(cookbookViewModel);
                    }
                }
                catch (Models.Errors.WebUserSafeException usEx)
                {
                    ModelState.AddModelError("CategoryIds", usEx.Message);
                    return View(cookbookViewModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CategoryIds", "An error occurred.");
                    return View(cookbookViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(cookbookViewModel);
        }


        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == id, includeProperties: "CommunityCookbooks,CookbookCategories");

            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting, Id does not exist. " });

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var item in objFromDb.CookbookCategories)
                    {
                        _unitOfWork.CookbookCategories.Remove(item);

                    }
                    _unitOfWork.Save();
                    _unitOfWork.Cookbook.Remove(objFromDb);
                    _unitOfWork.Save();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return Json(new { success = true, message = "Delete successful." });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Cookbook.GetAll() });
        }
    }
}