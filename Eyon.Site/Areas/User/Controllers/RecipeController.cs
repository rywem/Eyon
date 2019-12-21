using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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


        [HttpGet]
        public IActionResult GetUserRecipes()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return Json(new { data = _unitOfWork.Recipe.GetAllOwned(claims.Value) });
        }
    }
}