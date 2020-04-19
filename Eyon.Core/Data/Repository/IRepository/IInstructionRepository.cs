using Eyon.Models;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository.IRepository
{
    public interface IInstructionRepository : IRepository<Instruction>
    {

        void Update( Instruction instruction );
        //Task<bool> UpdateAsync( Instruction instruction );
    }    
}
