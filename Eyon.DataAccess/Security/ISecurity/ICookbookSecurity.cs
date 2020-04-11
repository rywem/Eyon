using Eyon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Security.ISecurity
{
    public interface ICookbookSecurity
    {
        Task UpdateAsync( string currentApplicationUserId, CookbookViewModel cookbookViewModel );
        Task AddAsync( string currentApplicationUserId, CookbookViewModel cookbookViewModel );
        Task<bool> DeleteAsync( string currentApplicationUserId, long id );
    }
}
