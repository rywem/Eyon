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
                var files = HttpContext.Request.Form.Files;
                string s = string.Empty;
                SiteImage image = null;
                if (categoryViewModel.Category.Id == 0)
                {
                    
                    if (files[0].Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[0].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            s = Convert.ToBase64String(fileBytes);
                        }
                        image = new SiteImage()
                        {
                            Title = files[0].FileName,
                            FileType = Path.GetExtension(files[0].FileName),
                            Encoded = s
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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Category.GetAll() });
        }
    }
}