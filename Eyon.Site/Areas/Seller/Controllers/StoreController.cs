﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Seller.Controllers
{
    [Authorize(Roles = Utilities.Statics.Roles.Admin + "," + Utilities.Statics.Roles.Manager + "," + Utilities.Statics.Roles.Seller)]
    [Area("Seller")]
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}