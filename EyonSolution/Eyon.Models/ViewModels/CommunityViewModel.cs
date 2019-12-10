using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class CommunityViewModel
    {
        public Community Community { get; set; }
        public long? StateId { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; }
        public CommunityViewModel()
        {
            Community = new Community();
        }
    }
}
