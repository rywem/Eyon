using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.DataAccess.Security;
using Eyon.DataAccess.Security.ISecurity;
using Eyon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," +
            Utilities.Statics.Roles.Seller + "," + Utilities.Statics.Roles.Customer + "," + Utilities.Statics.Roles.User)]
    public class FeedController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedSecurity _feedSecurity;
        [BindProperty]
        public FeedViewModel recipeViewModel { get; set; }

        public FeedController( IUnitOfWork unitOfWork, IFeedSecurity feedSecurity )
        {
            this._unitOfWork = unitOfWork;
            this._feedSecurity = feedSecurity;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                FeedViewModel feedViewModel = await this._feedSecurity.GetFeedAsync();
                return View(feedViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}