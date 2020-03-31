using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Eyon.Models;
using Eyon.DataAccess.Data.Repository;
using System.Threading.Tasks;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Eyon.Models.SiteObjects;
using Eyon.DataAccess.Security;

namespace Eyon.DataAccess.Data.Orchestrators
{
    public class CategoryOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task RunSync()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            foreach ( var category in categories )
            {
                //if ( state.FeedState != null && state.FeedState.Count > 0 )
                //    continue;
                if ( _unitOfWork.Topic.Any(x => x.ObjectId == category.Id && x.TopicType == category.TopicType) )
                    continue;

                _unitOfWork.Topic.AddFromITopicItem(category);
                await _unitOfWork.SaveAsync();
            }
        }

        internal async Task UpdateTransactionAsync(Category category )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await UpdateAsync(category);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        private async Task UpdateAsync( Category category )
        {
            if ( category.SiteImage != null )
            {
                _unitOfWork.SiteImage.Remove(category.SiteImageId);
                _unitOfWork.SiteImage.Update(category.SiteImage);
                await _unitOfWork.SaveAsync();
                category.SiteImageId = category.SiteImage.Id;
            }
            _unitOfWork.Category.Update(category);
            _unitOfWork.Topic.UpdateFromITopicItem(category);
            await _unitOfWork.SaveAsync();
        }

        internal async Task AddTransactionAsync( Category category )
        {
            using ( var transaction = _unitOfWork.BeginTransaction() )
            {
                try
                {
                    await AddAsync(category);
                    await transaction.CommitAsync();
                }
                catch ( Exception ex )
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        internal async Task AddAsync(Category category )
        {
            _unitOfWork.SiteImage.Add(category.SiteImage);
            await _unitOfWork.SaveAsync();
            category.SiteImageId = category.SiteImage.Id;            
            _unitOfWork.Category.Add(category);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Topic.AddFromITopicItem(category);
            await _unitOfWork.SaveAsync();
        }
    }
}
