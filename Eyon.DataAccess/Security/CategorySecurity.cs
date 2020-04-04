using Eyon.DataAccess.Data.Orchestrators;
using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Utilities.Extensions;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration _config;
        public CategorySecurity( IUnitOfWork unitOfWork, IConfiguration config )
        {
            this._unitOfWork = unitOfWork;
            this._categoryOrchestrator = new CategoryOrchestrator(this._unitOfWork);
            this._config = config;
        }

        public async Task AddAsync(Category category)
        {
            await _categoryOrchestrator.AddTransactionAsync(category);
        }

        public async Task UpdateAsync( Category category )
        {
            await _categoryOrchestrator.UpdateTransactionAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var data = await _unitOfWork.Category.GetAllAsync(includeProperties: "SiteImage");
            using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                        , _config.GetValue<string>("AWS:SecretKey")) )
            {                return data.GetImagesUrl(x => x.SiteImage.Thumb = service.GetPreSignedUrl(_config.GetValue<string>("AWS:Bucket"), x.SiteImage.FileNameThumb));
            }
        }
    }
}
