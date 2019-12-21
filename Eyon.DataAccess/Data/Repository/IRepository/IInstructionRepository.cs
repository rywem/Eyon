using Eyon.Models;

namespace Eyon.DataAccess.Data.Repository.IRepository
{
    public interface IInstructionRepository : IRepository<Instruction>
    {

        void Update( Instruction instruction );
    }    
}
