using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.Core.Orchestrators;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," + Utilities.Statics.Roles.Seller)]
    [Area("Seller")]
    public class OrganizationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private OrganizationOrchestrator organizationOrchestrator;
        [BindProperty]
        public OrganizationViewModel organizationViewModel { get; set; }
        public OrganizationController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this.organizationOrchestrator = new OrganizationOrchestrator(_unitOfWork);

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(long? id)
        {
            if ( id == null )
                organizationViewModel = organizationOrchestrator.CreateOrganizationViewModel();
            else
                organizationViewModel = organizationOrchestrator.GetOrganizationViewModel(id.GetValueOrDefault());

            return View(organizationViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Upsert()
        {
            if ( ModelState.IsValid )
            {
                //todo validate the user submitting has permission to add or edit this cookbook.
                try
                {
                    if ( organizationViewModel.Organization.Id == 0 ) //New cookbook
                    {
                        organizationOrchestrator.AddOrganizationTransaction(organizationViewModel);
                    }
                    else
                    {
                        organizationOrchestrator.UpdateOrganizationTransaction(organizationViewModel);
                    }
                }
                catch ( Eyon.Models.Errors.SafeException usEx )
                {
                    ModelState.AddModelError(string.Empty, usEx.Message);
                    return View(organizationViewModel);
                }
                catch ( Exception ex )
                {
                    ModelState.AddModelError(String.Empty, "An error occurred.");
                    return View(organizationViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(organizationViewModel);
        }

        [HttpDelete]
        public IActionResult Delete( long id )
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Organization.GetAll() });
        }
    }
}