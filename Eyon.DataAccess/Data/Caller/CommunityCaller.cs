using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Caller
{
    public class CommunityCaller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommunityCaller( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        

    }
}
