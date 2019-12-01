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
namespace Eyon.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Category Category { get; set; }        
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
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var files = HttpContext.Request.Form.Files;
                        string s = string.Empty;
                        if (Category.Id == 0)
                        {

                            if (files[0].Length > 0)
                            {
                                Category.SiteImage = new SiteImage()
                                {
                                    Title = Category.SiteImage.Title,
                                    Alt = Category.SiteImage.Alt,
                                    FileType = Path.GetExtension(files[0].FileName).Trim('.'),
                                    Encoded = files[0].ConvertToBase64()

                                };
                            }
                            else
                            {
                                ModelState.AddModelError("SiteImageId", "Please add an image file");
                                return View(Category);
                            }
                            _unitOfWork.SiteImage.Add(Category.SiteImage);
                            _unitOfWork.Save();
                            Category.SiteImageId = Category.SiteImage.Id;
                            _unitOfWork.Category.Add(Category);
                        }
                        else
                        {
                            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == Category.Id, includeProperties: "SiteImage");

                            categoryFromDb.SiteImage.Alt = Category.SiteImage.Alt;
                            categoryFromDb.SiteImage.Title = Category.SiteImage.Title;
                            if (files.Count > 0)
                            {
                                categoryFromDb.SiteImage.FileType = Path.GetExtension(files[0].FileName).Trim('.');
                                categoryFromDb.SiteImage.Encoded = files[0].ConvertToBase64();
                            }
                            Category.SiteImage = categoryFromDb.SiteImage;
                            _unitOfWork.SiteImage.Update(Category.SiteImage);
                            _unitOfWork.Save();
                            Category.SiteImageId = Category.SiteImage.Id;
                            _unitOfWork.Category.Update(Category);
                        }
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
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
                catch( Exception)
                {
                    transaction.Rollback();
                }
            }
            return Json(new { success = true, message = "Success" });

        }

        [HttpGet]
        public IActionResult GetAll()
        {            
            return Json(new { data = _unitOfWork.Category.GetAll(includeProperties:"SiteImage") });
        }
    }
}