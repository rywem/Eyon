using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eyon.Site.Extensions;
using Microsoft.AspNetCore.Authorization;
using Eyon.Site.WebUtilities;
using Eyon.Models.SiteObjects;

namespace Eyon.Site.Areas.Admin.Controllers
{
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager)]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly long _fileSizeLimit = 2097152;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".gif", ".png" };
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }       
        public IActionResult Upsert(long? id)
        {
            Category category = new Category();
            category.SiteImage = new SiteImage();

            if (id == null)
                return View(category);

            if (id != null)
            {
                category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id.GetValueOrDefault(), includeProperties: "SiteImage");
            }

            if (category == null || category.SiteImage == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category Category)
        {
            var files = HttpContext.Request.Form.Files;
            byte[] formFileContent;
            if( files != null && files.Count > 0)
                formFileContent = await FileHelpers.ProcessFormFileAsync<FileUpload>(files[0], ModelState, _permittedExtensions, _fileSizeLimit);
            if (ModelState.IsValid)
            {
                // If New
                if ( Category.Id == 0 )
                {

                }
                else
                {

                }

                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        //var files = HttpContext.Request.Form.Files;
                        //var formFileContent = await FileHelpers.ProcessFormFileAsync<FileUpload>(files[0], ModelState, _permittedExtensions, _fileSizeLimit);
                        string s = string.Empty;
                        if (Category.Id == 0)
                        {

                            if (files[0].Length > 0)
                            {
                                Category.SiteImage = new SiteImage()
                                {
                                    Description = Category.SiteImage.Description,
                                    Alt = Category.SiteImage.Alt,
                                    FileType = Path.GetExtension(files[0].FileName).Trim('.'),
                                    //Encoded = files[0].ConvertToBase64()
                                };
                            }
                            else
                            {
                                ModelState.AddModelError("SiteImageId", "Please add an image file");
                                return View(Category);
                            }
                            _unitOfWork.SiteImage.Add(Category.SiteImage);
                            await _unitOfWork.SaveAsync();
                            Category.SiteImageId = Category.SiteImage.Id;
                            _unitOfWork.Category.Add(Category);
                            _unitOfWork.Topic.AddFromITopicItem(Category);
                            await _unitOfWork.SaveAsync();
                        }
                        else
                        {
                            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == Category.Id, includeProperties: "SiteImage");
                            categoryFromDb.SiteImage.Alt = Category.SiteImage.Alt;
                            categoryFromDb.SiteImage.Description = Category.SiteImage.Description;
                            if (files.Count > 0)
                            {
                                categoryFromDb.SiteImage.FileType = Path.GetExtension(files[0].FileName).Trim('.');
                                //categoryFromDb.SiteImage.Encoded = files[0].ConvertToBase64();
                            }
                            Category.SiteImage = categoryFromDb.SiteImage;
                            _unitOfWork.SiteImage.Update(Category.SiteImage);
                            await _unitOfWork.SaveAsync();
                            Category.SiteImageId = Category.SiteImage.Id;
                            _unitOfWork.Category.Update(Category);
                        }
                        await _unitOfWork.SaveAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Category);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var categoryFromDb = _unitOfWork.Category.Get(id);

                    if (categoryFromDb == null)
                    {
                        return Json(new { success = false, message = "Error while deleting" });
                    }

                    var imageFromDb = _unitOfWork.SiteImage.Get(categoryFromDb.SiteImageId);
                    if (imageFromDb == null)
                    {
                        return Json(new { success = false, message = "Error while deleting" });
                    }

                    _unitOfWork.Category.Remove(categoryFromDb);
                    _unitOfWork.Save();
                    _unitOfWork.SiteImage.Remove(imageFromDb);
                    _unitOfWork.Save();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return Json(new { success = true, message = "Success" });

        }

        [HttpGet]
        [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
                    Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
        [Area("User")]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _unitOfWork.Category.GetAllAsync(includeProperties: "SiteImage") });
        }
        [HttpGet]
        [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
            Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
        [Area("User")]
        public IActionResult SearchCategories( string filter )
        {
            throw new NotImplementedException();
            var x = ( from p in _unitOfWork.Category.Search(filter, includeProperties: "SiteImage")
                      select new
                      {
                          name = p.Name,
                          displayOrder = p.DisplayOrder,
                          id = p.Id,
                          imageTitle = p.SiteImage.Description,
                          imageAlt = p.SiteImage.Alt,
                          //image = p.SiteImage.Image
                      } );


            return Json(new { categories = x });
        }

    }
}