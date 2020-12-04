using Eyon.Core.Data.Repository.IRepository;
using System.Threading.Tasks;
using System.Linq;
namespace Eyon.Core.Orchestrators
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
                if ( _unitOfWork.Topic.Any(x => x.ObjectId == country.Id && x.TopicType == country.TopicType) )
                    continue;

                _unitOfWork.Topic.AddFromITopicItem(country);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
