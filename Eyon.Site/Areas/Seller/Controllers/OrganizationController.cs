using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
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
        public OrganizationController()
        {

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
    }
}