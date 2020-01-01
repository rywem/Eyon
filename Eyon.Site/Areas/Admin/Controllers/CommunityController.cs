using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Eyon.DataAccess.Data.Orchestrators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.AccessControl;
using Eyon.DataAccess.SeedData.Location;
using CsvHelper;

namespace Eyon.Site.Areas.Admin.Controllers
{

    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager)]
    [Area("Admin")]
    public class CommunityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private CommunityOrchestrator _communityOrchestrator;
        //[BindProperty]
        //public CommunityViewModel communityViewModel { get; set; }

        public CommunityController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this._communityOrchestrator = new CommunityOrchestrator(_unitOfWork);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Upsert( long? id )
        {
            CommunityViewModel communityViewModel = new CommunityViewModel();
            if ( id == null || id == 0 )
            {
                communityViewModel = _communityOrchestrator.CreateCommunityViewModel();
                return View(communityViewModel);
            }
            communityViewModel = _communityOrchestrator.GetCommunityViewModel(id.GetValueOrDefault());
            if ( communityViewModel.Community == null )
                return NotFound();

            return View(communityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert( CommunityViewModel communityViewModel )
        {
            if ( ModelState.IsValid )
            {
                if ( communityViewModel.Community != null )
                {
                    if ( communityViewModel.Community.Id == 0 )
                        _communityOrchestrator.AddCommunityTransaction(communityViewModel);
                    else
                        _communityOrchestrator.UpdateCommunityTransaction(communityViewModel);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(communityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Utilities.Statics.Roles.Admin)]
        public async Task<IActionResult> Upload(IFormCollection formCollection)        
        {
            return RedirectToAction("Error", "Denied");
            if ( ModelState.IsValid)
            {                                
                var files = HttpContext.Request.Form.Files;
                if ( files[0].Length > 0 && files[0].Length < 2097152 )
                {                    
                    using ( var stream = new MemoryStream())
                    {                                                
                        await files[0].CopyToAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);                        
                        var records = Eyon.DataAccess.SeedData.Location.ZipCodeFile.LoadZipcodesFromStream(stream, true);
                        await _communityOrchestrator.UploadCommunities(records, 192);
                    }
                }
            }
            return View();
        }

        [Authorize(Roles = Utilities.Statics.Roles.Admin)]
        public IActionResult Upload()
        {
            return RedirectToAction("Error", "Denied");
            return View();
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
        public IActionResult Delete( int id )
        {
            var objFromDb = _unitOfWork.Community.Get(id);

            if ( objFromDb == null )
                return Json(new { success = false, message = "Error while deleting, Id does not exist." });

            _unitOfWork.Community.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful." });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var communities = _unitOfWork.Community.GetAll(includeProperties: "Country,CommunityState,CommunityState.State");
            var data = from c in communities
                       select new
                       {
                           name = c.Name,
                           stateProvince = c.CommunityState != null ? c.CommunityState.State.Name : "N/A",
                           country = c.Country.Name,
                           id = c.Id
                       };
            return Json(new { data });
        }

        [HttpGet]
        [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," + Utilities.Statics.Roles.Seller)]
        public IActionResult GetStates( long countryId )
        {
            return Json(new { data = _unitOfWork.State.GetAll(x => x.CountryId == countryId) });
        }
    }
}