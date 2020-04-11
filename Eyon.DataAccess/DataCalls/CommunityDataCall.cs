using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.DataCalls
{
    public class CommunityDataCall
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommunityDataCall( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

    }
}
