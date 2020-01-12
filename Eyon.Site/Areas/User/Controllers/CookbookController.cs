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
using System.Security.Claims;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Area("User")]
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
           Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
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

        public IActionResult View(long id)
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

            try
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if( cookbookViewModel.Cookbook.Id != 0 )
                {
                    bool isOwner = _unitOfWork.Cookbook.IsOwner(claims.Value, cookbookViewModel.Cookbook.Id);
                    if ( isOwner == false )
                    {
                        ModelState.AddModelError("Recipe.Id", "An error occurred.");
                        return RedirectToAction("Denied", "Error");
                    }
                }

                if ( ModelState.IsValid )
                {
                    //todo validate the user submitting has permission to add or edit this cookbook.
                    try
                    {

                        if ( cookbookViewModel.Cookbook.Id == 0 ) //New cookbook
                        {
                            cookbookOrchestrator.AddCookbookTransaction(claims.Value, cookbookViewModel);
                        }
                        else
                        {
                            cookbookOrchestrator.UpdateCookbookTransaction(claims.Value, cookbookViewModel);
                        }
                    }
                    catch ( Models.Errors.WebUserSafeException usEx )
                    {
                        ModelState.AddModelError("CategoryIds", usEx.Message);
                        return View(cookbookViewModel);
                    }
                    catch ( Exception ex )
                    {
                        ModelState.AddModelError("CategoryIds", "An error occurred.");
                        return View(cookbookViewModel);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch ( Exception ex )
            {
                throw ex;
            }
            return View(cookbookViewModel);
        }


        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefaultOwned(claims.Value, x => x.Id == id, includeProperties: "CommunityCookbook,CookbookCategory,ApplicationUserOwner");

            if (objFromDb == null)
                return Json(new { success = false, message = "An error occurred." });

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var item in objFromDb.CookbookCategory)
                    {
                        _unitOfWork.CookbookCategory.Remove(item);

                    }
                    foreach ( var item in objFromDb.ApplicationUserOwner )
                    {
                        _unitOfWork.ApplicationUserCookbook.Remove(item);
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
        public async Task<IActionResult> GetAll()
        {            
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return Json(new { data = await _unitOfWork.Cookbook.GetAllOwnedAsync(claims.Value) });
        }

        [HttpGet]
        public IActionResult GetUserCookbooks()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return Json(new { data = _unitOfWork.Cookbook.GetAllOwned(claims.Value) });
        }
    }
}