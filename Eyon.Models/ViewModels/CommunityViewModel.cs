//using Microsoft.AspNetCore.Mvc.Rendering;
using Eyon.Models.SiteObjects;
using System.Collections.Generic;

namespace Eyon.Models.ViewModels
{
    public class CommunityViewModel
    {
        public Community Community { get; set; }
        public long? StateId { get; set; }
        public IEnumerable<SelectBoxItem> CountryList { get; set; }
        public IEnumerable<SelectBoxItem> StateList { get; set; }
        public CommunityViewModel()
        {
            Community = new Community();
        }
    }
}
