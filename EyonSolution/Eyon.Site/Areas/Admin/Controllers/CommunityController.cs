using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CommunityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Community Community { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        [Area("Seller")]
        public IActionResult Add()
        {
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
    }
}