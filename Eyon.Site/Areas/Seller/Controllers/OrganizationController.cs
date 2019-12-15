using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Authorize(Roles = "Admin,Manager,Seller")]
    [Area("Seller")]
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}