using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eyon.Core.Orchestrators;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Core.Images;
using Eyon.Core.Security;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.SiteObjects;
using Eyon.Models.ViewModels;
using Eyon.Site.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Eyon.Core.Security.ISecurity;

namespace Eyon.Site.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
            Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
    public class RecipeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeSecurity _recipeSecurity;
        private readonly long _fileSizeLimit = 8388608;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".gif", ".png" };
        private readonly IImageHelper _imageHelper;
        
        [BindProperty]
        public RecipeViewModel recipeViewModel { get; set; }

        public RecipeController( IUnitOfWork unitOfWork, IRecipeSecurity recipeSecurity, IImageHelper imageHelper )
        {
            this._unitOfWork = unitOfWork;
            this._recipeSecurity = recipeSecurity;
            this._imageHelper = imageHelper;
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
                recipeViewModel = await _recipeSecurity.GetAsync(claims.Value, id);
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
                recipeViewModel = await _recipeSecurity.GetAsync(claims.Value, id.GetValueOrDefault());
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

                List<byte[]> filesAsByteArrays = new List<byte[]>();

                if ( files != null && files.Count > 0 )
                {
                    foreach ( var  item in files )
                    {
                        filesAsByteArrays.Add(await FileHelpers.ProcessFormFileAsync<FileUpload>(item, ModelState, _permittedExtensions, _fileSizeLimit));
                    }
                }
                if ( ModelState.IsValid )
                {                    
                    if ( filesAsByteArrays.Count > 0)
                    {
                        for ( int i = 0; i < filesAsByteArrays.Count; i++ )
                        {
                            if ( filesAsByteArrays[i].Length > 0 )
                            {
                                if ( recipeViewModel.UserImage == null )
                                    recipeViewModel.UserImage = new List<UserImage>();
                                UserImage img = (UserImage)await _imageHelper.ProcessIImageFile(filesAsByteArrays[i], new UserImage());
                                img.Description = recipeViewModel.Recipe.Description;
                                img.Name = recipeViewModel.Recipe.Name;
                                recipeViewModel.UserImage.Add(img);
                            }
                        }
                    }
                    
                    try
                    {
                        if ( recipeViewModel.Recipe.Id == 0 ) //New cookbook
                        {

                            await _recipeSecurity.AddAsync(claims.Value, recipeViewModel);
                        }
                        else
                        {
                            await _recipeSecurity.UpdateAsync(claims.Value, recipeViewModel);
                        }
                    }
                    catch ( Eyon.Models.Errors.SafeException usEx )
                    {
                        // TODO log exception
                        throw usEx;
                    }
                    catch ( Exception ex )
                    {
                        // TODO log exception
                        throw ex;
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch ( Exception ex )
            {
                // TODO log exception
                throw ex;                
            }
            recipeViewModel.UserImage = null;
            return View(recipeViewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id )
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                await _recipeSecurity.DeleteAsync(claims.Value, id);
            }
            catch ( SafeException exception )
            {
                ModelState.AddModelError("Recipe.Id", "An error occurred");
                if ( exception.ErrorType == Models.Enums.ErrorType.Denied )
                    return RedirectToAction("Denied", "Error");
                // TODO log exception
                throw exception;
            }
            catch (Exception ex )
            {
                // TODO log exception
                throw ex;
            }
            return Json(new { success = true, message = "Delete successful." });
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