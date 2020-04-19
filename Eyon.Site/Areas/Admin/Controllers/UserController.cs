using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Utilities.Statics.Roles.Admin)]
    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;        
        public UserController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;            
        }
        public IActionResult Index()
        {
            // Todo check current user credentials
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); 

            return View(_unitOfWork.ApplicationUser.GetAll(x => x.Id != claims.Value ));
        }

        public IActionResult Lock( string id )
        {
            
            // Todo check current user credentials
            if ( id == null )
                return NotFound();

            _unitOfWork.ApplicationUser.LockUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Unlock( string id )
        {
            // Todo check current user credentials
            if ( id == null )
                return NotFound();

            _unitOfWork.ApplicationUser.UnlockUser(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Adds a new employee
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            throw new NotImplementedException();
        }
    }
}