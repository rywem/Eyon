using Eyon.DataAccess.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Data.Repository
{
    public class InstructionRepository : Repository<Instruction>, IInstructionRepository
    {
        private readonly ApplicationDbContext _db;

        public InstructionRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public void Update( Instruction instruction )
        {
            var objFromDb = _db.Instruction.FirstOrDefault(s => s.Id == instruction.Id);
            objFromDb.StepNumber = instruction.StepNumber;
            objFromDb.Text = instruction.Text;
            
            _db.SaveChanges();
        }

        public async Task<bool> UpdateAsync( Instruction instruction )
        {
            var objFromDb = _db.Instruction.FirstOrDefault(s => s.Id == instruction.Id);

            if ( objFromDb == null )
                return false;
            objFromDb.StepNumber = instruction.StepNumber;
            objFromDb.Text = instruction.Text;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}