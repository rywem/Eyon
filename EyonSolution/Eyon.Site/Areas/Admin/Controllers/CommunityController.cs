using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CommunityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public CommunityStateCountryViewModel communityStateCountryViewModel { get; set; }

        public CommunityController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Area("Seller")]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Upsert(long? id)
        {
            Community community = new Community();

            if (id == null)
                return View(community);

            community = _unitOfWork.Community.Get(id.GetValueOrDefault());

            if (community == null)
                return NotFound();

            return View(community);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            /*if (ModelState.IsValid)
            {
                if (community.Id == 0)
                    _unitOfWork.Community.Add(community);
                else
                    _unitOfWork.Community.Update(community);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            */
            return View(communityStateCountryViewModel);
        }

        public IActionResult Submit()
        {
            return View();
        }

        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Approve()
        {
            return View();
        }
        public IActionResult Reject()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Community.Get(id);

            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting, Id does not exist. " });

            _unitOfWork.Community.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful." });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Community.GetAll() });
        }
    }
}