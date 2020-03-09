using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eyon.Site.Areas.Admin.Controllers
{
    [Authorize(Roles = Utilities.Statics.Roles.Admin)]
    [Area("Admin")]
    public class SystemController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private CommunityOrchestrator _communityOrchestrator;
        private CountryOrchestrator _countryOrchestrator;
        private StateOrchestrator _stateOrchestrator;
        private CategoryOrchestrator _categoryOrchestrator;
        public SystemController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this._communityOrchestrator = new CommunityOrchestrator(unitOfWork);
            this._countryOrchestrator = new CountryOrchestrator(unitOfWork);
            this._stateOrchestrator = new StateOrchestrator(unitOfWork);
            this._categoryOrchestrator = new CategoryOrchestrator(unitOfWork);
        }
 
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Sync()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SyncTopic( TopicType topicType )
        {
            //TopicType topicType = (TopicType)topicId;
            switch ( topicType )
            {
                case Models.Enums.TopicType.Community:
                    await _communityOrchestrator.RunSync();
                    break;
                case Models.Enums.TopicType.Category:
                    await _categoryOrchestrator.RunSync();
                    break;
                case Models.Enums.TopicType.State:
                    await _stateOrchestrator.RunSync();
                    break;
                case Models.Enums.TopicType.Country:
                    await _countryOrchestrator.RunSync();
                    break;
                case Models.Enums.TopicType.Profile:                    
                case Models.Enums.TopicType.Cookbook:                
                case Models.Enums.TopicType.Recipe:                
                case Models.Enums.TopicType.Organization:
                    throw new NotImplementedException();
                    //break;
                default:
                    break;
            }
            return View();
        }
    }
}