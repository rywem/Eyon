using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Initializers
{
    public class DbInitializer : IDbInitializer
    {
        //TODO: 
        // https://www.udemy.com/course/master-aspnet-core-3-advanced/learn/lecture/15619672#overview

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        /*
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

        }
        */
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
