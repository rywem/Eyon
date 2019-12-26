using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
            Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
    public class RecipeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private RecipeOrchestrator recipeOrchestrator;

        [BindProperty]
        public RecipeViewModel recipeViewModel { get; set; }

        public RecipeController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this.recipeOrchestrator = new RecipeOrchestrator(_unitOfWork);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(long? id)
        {
            if ( id == null)
            {
                recipeViewModel = new RecipeViewModel();
            }

            return View(recipeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert( FormCollection formCollection )
        {
            if ( ModelState. IsValid )
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                //todo validate the user submitting has permission to add or edit this cookbook.
                try
                {
                    if ( recipeViewModel.Recipe.Id == 0 ) //New cookbook
                    {

                        recipeOrchestrator.AddRecipeTransaction(recipeViewModel, claims.Value);
                    }
                    else
                    {
                        //cookbookOrchestrator.UpdateCookbookTransaction(cookbookViewModel);
                    }
                }
                catch ( Models.Errors.WebUserSafeException usEx )
                {
                    throw usEx;
                    //ModelState.AddModelError("CategoryIds", usEx.Message);
                   // return View(cookbookViewModel);
                }
                catch ( Exception ex )
                {
                    throw ex;
                    //ModelState.AddModelError("CategoryIds", "An error occurred.");
                    //return View(cookbookViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(recipeViewModel);
        }
        [HttpGet]
        public IActionResult GetUserRecipes()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return Json(new { data = _unitOfWork.Recipe.GetAllOwned(claims.Value) });
        }        
    }
}