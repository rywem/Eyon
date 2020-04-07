using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Eyon.DataAccess.Orchestrators;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Eyon.Utilities.Extensions;
using Eyon.DataAccess.Security;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Area("User")]
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
           Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
    public class CookbookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private CookbookOrchestrator _cookbookOrchestrator;
        private CookbookSecurity _cookbookSecurity;
        [BindProperty]
        public CookbookViewModel cookbookViewModel { get; set; }
        public CookbookController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._cookbookOrchestrator = new CookbookOrchestrator(_unitOfWork);
            this._cookbookSecurity = new CookbookSecurity(_unitOfWork);
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
                cookbookViewModel = _cookbookOrchestrator.GetCookbookViewModel(id.GetValueOrDefault());
            }
            return View(cookbookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {

            try
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                //if( cookbookViewModel.Cookbook.Id != 0 )
                //{
                //    bool isOwner = await _unitOfWork.Cookbook.IsOwnerAsync(claims.Value, cookbookViewModel.Cookbook.Id);
                //    if ( isOwner == false )
                //    {
                //        ModelState.AddModelError("Recipe.Id", "An error occurred.");
                //        return RedirectToAction("Denied", "Error");
                //    }
                //}

                if ( ModelState.IsValid )
                {
                    //todo validate the user submitting has permission to add or edit this cookbook.
                    try
                    {
                        if ( cookbookViewModel.Cookbook.Id == 0 ) //New cookbook
                        {
                            await _cookbookSecurity.AddAsync(claims.Value, cookbookViewModel);
                        }
                        else
                        {
                            //_cookbookOrchestrator.UpdateCookbookTransaction(claims.Value, cookbookViewModel);
                            await _cookbookSecurity.UpdateAsync(claims.Value, cookbookViewModel);
                        }
                    }
                    catch ( Eyon.Models.Errors.SafeException usEx )
                    {
                        // TODO log exception
                        ModelState.AddModelError("CategoryIds", usEx.Message);
                        return View(cookbookViewModel);
                    }
                    catch ( Exception ex )
                    {
                        // TODO log exception
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
        public async Task<IActionResult> Delete(long id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            bool result = await _cookbookSecurity.DeleteAsync(claims.Value, id);
            
            if ( result  )
                return Json(new { success = result, message = "Delete successful." });
            else
                return Json(new { success = result, message = "An error occurred." });

            //var objFromDb = _unitOfWork.Cookbook.GetFirstOrDefaultOwned(claims.Value, x => x.Id == id, includeProperties: "CommunityCookbook,CookbookCategory,ApplicationUserOwner,FeedCookbook,FeedCookbook.Feed");

            //if (objFromDb == null)
            //    return Json(new { success = false, message = "An error occurred." });

            //// TODO delete feed and topic related database entries
            //using (var transaction = _unitOfWork.BeginTransaction())
            //{
            //    try
            //    {
            //        foreach (var item in objFromDb.CookbookCategory)
            //        {
            //            _unitOfWork.CookbookCategory.Remove(item);

            //        }
            //        foreach ( var item in objFromDb.ApplicationUserOwner )
            //        {
            //            _unitOfWork.ApplicationUserCookbook.Remove(item);
            //        }
            //        _unitOfWork.Save();
            //        _unitOfWork.Cookbook.Remove(objFromDb);
            //        _unitOfWork.Save();
            //        transaction.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        transaction.Rollback();
            //    }
            //}
            //return Json(new { success = true, message = "Delete successful." });
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

            var cookbooks = _unitOfWork.Cookbook.GetAllOwned(claims.Value, includeProperties: "CookbookRecipe");
            
            var data = from c in cookbooks
                       select new
                       {
                           c.Id,
                           c.Name,
                           Privacy = c.Privacy.ToString(),
                           c.Author,
                           c.Copyright,
                           c.ISBN,
                           Created = c.CreationDateTime.ToFriendlyString(),
                           Modified = c.ModifiedDateTime.ToFriendlyString(),
                           RecipeCount = c.CookbookRecipe == null ? 0 : c.CookbookRecipe.Count
                       };
            return Json(new { data = data });
            //return Json(new { data = _unitOfWork.Cookbook.GetAllOwned(claims.Value) });
        }
    }
}