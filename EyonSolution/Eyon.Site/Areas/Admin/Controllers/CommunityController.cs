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
        public CommunityViewModel communityViewModel { get; set; }

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
            if (ModelState.IsValid)
            {
                //Community community = new Community();
                communityViewModel = new CommunityViewModel();
                if (id == null)
                {
                    communityViewModel.CountryList = _unitOfWork.Country.GetCountryListForDropDown();
                    communityViewModel.StateList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                    return View(communityViewModel);
                }
                communityViewModel.Community = _unitOfWork.Community.GetFirstOrDefault(x => x.Id == id.GetValueOrDefault(), includeProperties: "Country,CommunityState");

                if (communityViewModel.Community == null)
                    return NotFound();
                else
                {
                    communityViewModel.CountryList = _unitOfWork.Country.GetCountryListForDropDown();
                    communityViewModel.StateList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                }

            }
            return View(communityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {            
            throw new NotImplementedException();
            long? id = communityViewModel.StateId;

            //To do, only admins can approve a community. 
            return View(communityViewModel);
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

        [HttpGet]
        public IActionResult GetStates(long countryId)
        {
            return Json(new { data = _unitOfWork.State.GetAll(x => x.CountryId == countryId )});
        }
    }
}