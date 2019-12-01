using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class CookbookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Cookbook cookbook { get; set; }
        public CookbookController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(long? id)
        {
            Cookbook cookbook = new Cookbook();            
            if (id == null)
                return View(cookbook);

            if (id != null)
            {
                cookbook = _unitOfWork.Cookbook.GetFirstOrDefault(x => x.Id == id.GetValueOrDefault());
            }

            if (cookbook == null )
                return NotFound();
            return View(cookbook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (cookbook.Id == 0)
                    _unitOfWork.Cookbook.Add(cookbook);
                else
                    _unitOfWork.Cookbook.Update(cookbook);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cookbook);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Cookbook.Get(id);

            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting, Id does not exist. " });

            _unitOfWork.Cookbook.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful." });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Cookbook.GetAll() });
        }
    }
}