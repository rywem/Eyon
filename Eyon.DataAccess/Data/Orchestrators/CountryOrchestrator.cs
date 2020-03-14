using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Eyon.DataAccess.Data.Orchestrators
{
    public class CountryOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryOrchestrator( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task RunSync()
        {
            var countries = await _unitOfWork.Country.GetAllAsync();

            foreach ( var country in countries.ToList() )
            {
                //if ( country.FeedCountry != null && country.FeedCountry.Count > 0 )
                //    continue;

                if ( _unitOfWork.Topic.Any(x => x.ObjectId == country.Id && x.TopicType == country.TopicType) )
                    continue;

                _unitOfWork.Topic.AddFromITopicItem(country);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
