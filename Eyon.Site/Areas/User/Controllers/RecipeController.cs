using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Eyon.Site.Extensions;
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

        public async Task<IActionResult> View(long id)
        {
            RecipeViewModel recipeViewModel = null;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            try
            {                               
                ViewBag.id = id;
                recipeViewModel = await recipeOrchestrator.GetRecipeViewModelAsync(id, claims.Value);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View(recipeViewModel);
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
                recipeViewModel = await recipeOrchestrator.GetRecipeViewModelAsync(id.GetValueOrDefault(), claims.Value);
            }

            return View(recipeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if ( recipeViewModel.Recipe.Id != 0 )
                {                    
                    bool isOwner = await _unitOfWork.Recipe.IsOwnerAsync(claims.Value, recipeViewModel.Recipe.Id);
                    if ( isOwner == false )
                    {
                        ModelState.AddModelError("Recipe.Id", "An error occurred.");
                        return RedirectToAction("Denied", "Error");
                    }
                }
                var files = HttpContext.Request.Form.Files;
                if ( ModelState.IsValid )
                {                    
                    if ( files.Count > 0)
                    {
                        for ( int i = 0; i < files.Count; i++ )
                        {
                            if (files[i].Length > 0 )
                            {
                                if ( recipeViewModel.UserImage == null )
                                    recipeViewModel.UserImage = new List<UserImage>();
                                recipeViewModel.UserImage.Add(new UserImage()
                                {
                                    Encoded = files[i].ConvertToBase64(),
                                    FileType = Path.GetExtension(files[i].FileName).Trim('.'),
                                    Description = string.Empty
                                });
                            }
                        }
                    }
                    // Create Ingredients
                    string[] ingredientsSplit = recipeViewModel.IngredientsText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    recipeViewModel.Ingredient = new List<Ingredient>();
                    int step = 1;
                    foreach ( var item in ingredientsSplit )
                    {
                        recipeViewModel.Ingredient.Add(new Ingredient()
                        {
                            Text = item,
                            Number = step,
                            RecipeId = recipeViewModel.Recipe.Id
                        });
                        step++;
                    }
                    // Create Instructions
                    string[] instructionsSplit = recipeViewModel.InstructionsText.Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);

                    step = 1;
                    recipeViewModel.Instruction = new List<Instruction>();
                    foreach ( var item in instructionsSplit )
                    {
                        recipeViewModel.Instruction.Add(new Instruction()
                        {
                            StepNumber = step,
                            Text = item,
                            RecipeId = recipeViewModel.Recipe.Id
                        });
                        step++;
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
                    catch ( Models.Errors.SafeException usEx )
                    {
                        throw usEx;                        
                    }
                    catch ( Exception ex )
                    {
                        throw ex;
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch ( Exception ex )
            {
                throw ex;                
            }
            recipeViewModel.UserImage = null;
            return View(recipeViewModel);
        }

        [HttpGet]
        public IActionResult GetUserRecipes()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return Json(new { data = _unitOfWork.Recipe.GetAllOwned(claims.Value) });
        }

        [HttpGet]
        public IActionResult GetAvailableRecipes()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return Json(new { data = _unitOfWork.Recipe.GetAllAvailable(claims.Value) });
        }
    }
}