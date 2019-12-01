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
        public CategoryViewModel categoryViewModel { get; set; }
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

            CategoryViewModel categoryViewModel = new CategoryViewModel()
            {
                Category = new Models.Category(),
                SiteImage = new Models.SiteImage()
            };

            if (id == null)
                return View(categoryViewModel);

            if (id != null)
            {
                categoryViewModel.Category = _unitOfWork.Category.Get(id.GetValueOrDefault());
                categoryViewModel.SiteImage = _unitOfWork.SiteImage.Get(categoryViewModel.Category.SiteImageId);
            }

            if (categoryViewModel.Category == null || categoryViewModel.SiteImage == null)
                return NotFound();

            return View(categoryViewModel);
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
                        SiteImage image = null;
                        if (categoryViewModel.Category.Id == 0)
                        {

                            if (files[0].Length > 0)
                            {
                                image = new SiteImage()
                                {
                                    Title = categoryViewModel.SiteImage.Title,
                                    Alt = categoryViewModel.SiteImage.Alt,
                                    FileType = Path.GetExtension(files[0].FileName),
                                    Encoded = files[0].ConvertToBase64()

                                };
                            }
                            else
                            {
                                ModelState.AddModelError("SiteImageId", "Please add an image file");
                                return View(categoryViewModel);
                            }
                            _unitOfWork.SiteImage.Add(image);
                            _unitOfWork.Save();
                            categoryViewModel.Category.SiteImageId = image.Id;
                            _unitOfWork.Category.Add(categoryViewModel.Category);
                        }
                        else
                        {
                            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == categoryViewModel.Category.Id, includeProperties: "SiteImage");

                            if (files != null && files[0].Length > 0)
                            {
                                image = CreateImage(files[0]);
                                _unitOfWork.SiteImage.Add(image);
                                _unitOfWork.Save();
                                categoryViewModel.Category.SiteImageId = image.Id;
                            }
                            _unitOfWork.Category.Update(categoryViewModel.Category);
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
            return View(categoryViewModel);
        }

        private SiteImage CreateImage(IFormFile file)
        {
            string s;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                s = Convert.ToBase64String(fileBytes);
            }
            return new SiteImage()
            {
                Title = file.FileName,
                FileType = Path.GetExtension(file.FileName),
                Encoded = s
            };
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
            return Json(new { success = true, message = "Success!" });

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Category.GetAll() });
        }
    }
}