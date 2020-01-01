using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Eyon.Models;
//using SampleApp.Models;
//using SampleApp.Utilities;

namespace Eyon.Site.Areas.Admin.Controllers
{
    //https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/mvc/models/file-uploads.md
    public class StreamingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}