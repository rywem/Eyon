using Eyon.Models;
using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.Core.Orchestrators.IOrchestrator
{
    public interface ICookbookOrchestrator
    {
        CookbookViewModel Get( long id );
        Task AddTransactionAsync( string currentUserId, CookbookViewModel cookbookViewModel );
        Task AddAsync( string currentUserId, CookbookViewModel cookbookViewModel );

        Task UpdateTransactionAsync( string currentUserId, CookbookViewModel cookbookViewModel );
        Task UpdateAsync( string currentUserId, CookbookViewModel cookbookViewModel );
        Task DeleteTransactionAsync( string currentApplicationUserId, Cookbook objFromDb );
        Task DeleteAsync( string currentApplicationUserId, Cookbook objFromDb );
    }
}
