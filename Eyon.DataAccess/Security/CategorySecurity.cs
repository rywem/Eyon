using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Security
{
    public class CategorySecurity
    {
        private readonly IUnitOfWork _unitOfWork;
        private CategoryOrchestrator _categoryOrchestrator;

        public CategorySecurity( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
            this._categoryOrchestrator = new CategoryOrchestrator(this._unitOfWork);
        }

        public async Task AddAsync(Category category)
        {
            await _categoryOrchestrator.AddTransactionAsync(category);
        }

        public async Task UpdateAsync( Category category )
        {
            await _categoryOrchestrator.UpdateTransactionAsync(category);
        }
    }
}
