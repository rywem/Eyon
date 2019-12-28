using Eyon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eyon.DataAccess.Data.Initializers
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public void Initialize()
        {
            try
            {
                if ( _db.Database.GetPendingMigrations().Count() > 0 )
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex )
            {

            }
           
            // If no pending migrations
            if ( _db.Roles.Any(r => r.Name == Eyon.Utilities.Statics.Roles.Admin) )
            {
                return;
            }
            // Create roles 
            _roleManager.CreateAsync(new IdentityRole(Eyon.Utilities.Statics.Roles.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Eyon.Utilities.Statics.Roles.Manager)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Eyon.Utilities.Statics.Roles.Seller)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Eyon.Utilities.Statics.Roles.Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Eyon.Utilities.Statics.Roles.User)).GetAwaiter().GetResult();
            // Create Users
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "ryan.wemmer@gmail.com",
                Email = "ryan.wemmer@gmail.com",
                EmailConfirmed  = true,  
                FirstName = "Ryan",
                LastName = "Wemmer",
            }, "M_g2parma").GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "cybervoid41@gmail.com",
                Email = "cybervoid41@gmail.com",
                EmailConfirmed = true,
                FirstName = "Ryan",
                LastName = "Wemmer",
            }, "M_g2parma").GetAwaiter().GetResult();
            // Assign roles
            ApplicationUser user = _db.ApplicationUser.Where(u => u.Email == "ryan.wemmer@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, Utilities.Statics.Roles.Admin).GetAwaiter().GetResult();

            ApplicationUser user2 = _db.ApplicationUser.Where(u => u.Email == "cybervoid41@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user2, Utilities.Statics.Roles.Customer).GetAwaiter().GetResult();
        }
    }
}
