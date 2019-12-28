using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
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

        
        //public RecipeViewModel recipeViewModel { get; set; }

        public RecipeController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this.recipeOrchestrator = new RecipeOrchestrator(_unitOfWork);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(long? id)
        {
            RecipeViewModel recipeViewModel;
            ViewBag.Id = 0;
            if ( id == null)
            {
                recipeViewModel = new RecipeViewModel();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                bool isOwner = await _unitOfWork.Recipe.IsOwnerAsync(claims.Value, id.GetValueOrDefault());

                if ( isOwner == false )
                    return RedirectToAction("Denied", "Error");

                ViewBag.id = id.GetValueOrDefault();
                recipeViewModel = recipeOrchestrator.GetRecipeViewModel(id.GetValueOrDefault(), claims.Value);
            }

            return View(recipeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert( IFormCollection form, long id )
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            try
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if ( id != 0 )
                {                    
                    bool isOwner = await _unitOfWork.Recipe.IsOwnerAsync(claims.Value, id);
                    if ( isOwner == false )
                    {
                        ModelState.AddModelError("Recipe.Id", "An error occurred.");
                        return RedirectToAction("Denied", "Error");
                    }
                }
                
                if ( id != 0 )
                {
                    recipeViewModel.Recipe = await _unitOfWork.Recipe.GetFirstOrDefaultOwnedAsync(claims.Value, x => x.Id == id);
                    if ( recipeViewModel.Recipe == null || recipeViewModel.Recipe.Id == 0 )
                        ModelState.AddModelError("Recipe.Id", "An error occurred.");
                }                

                if ( ModelState.IsValid )
                {
                    // Create the RecipeViewModel
                    recipeViewModel.Recipe.Name = form["Recipe.Name"];
                    recipeViewModel.Recipe.Cooktime = form["Recipe.Cooktime"];                    
                    List<Instruction> instructionsUnordered = new List<Instruction>();
                    foreach ( var item in form.Keys )
                    {
                        if ( item.Contains("instruction_"))
                        {
                            int step;                            
                            string[] tempInstructionArray = form[item].ToString().Split(',', 2, options: StringSplitOptions.RemoveEmptyEntries);
                            if ( !int.TryParse(tempInstructionArray[0], out step) )
                            {
                                ModelState.AddModelError("Instructions", "Invalid Instructions");
                                break;
                            }

                            instructionsUnordered.Add(new Instruction()
                            {
                                StepNumber = step,
                                Text = tempInstructionArray[1],
                                RecipeId = recipeViewModel.Recipe.Id
                            });
                        }
                    }
                    
                    int stepCounter = 1;
                    foreach ( var item in instructionsUnordered.OrderBy(x => x.StepNumber) )
                    {
                        item.StepNumber = stepCounter;
                        recipeViewModel.Instructions.Add(item);
                        stepCounter++;
                    }
                    
                    try
                    {
                        if ( recipeViewModel.Recipe.Id == 0 ) //New cookbook
                        {

                            await recipeOrchestrator.AddRecipeTransactionAsync(claims.Value, recipeViewModel);
                        }
                        else
                        {
                            await recipeOrchestrator.UpdateRecipeTransactionAsync(claims.Value, recipeViewModel);
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
            }
            catch ( Exception ex )
            {
                throw ex;                
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