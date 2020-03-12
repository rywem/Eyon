﻿using Eyon.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
        IEnumerable<SelectListItem> GetStateListForDropDown(long countryId);
    }
}
