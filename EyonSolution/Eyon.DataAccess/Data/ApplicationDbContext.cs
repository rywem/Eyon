using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eyon.Models;
namespace Eyon.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<SiteImage> SiteImage { get; set; }
        public DbSet<Community> Community { get; set; }
        public DbSet<Cookbook> Cookbook { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Recipe> Recipe { get; set; }

        #region Relationship Tables
        public DbSet<Models.Relationship.CommunityOrganizations> CommunityOrganizations { get; set; }
        public DbSet<Models.Relationship.CommunityCookbooks> CommunityCookbooks { get; set; }
        public DbSet<Models.Relationship.CookbookCategories> CookbookCategories { get; set; }
        public DbSet<Models.Relationship.CookbookRecipes> CookbookRecipes { get; set; }
        public DbSet<Models.Relationship.CookbookSiteImages> CookbookSiteImages { get; set; }
        public DbSet<Models.Relationship.OrganizationCookbooks> OrganizationCookbooks { get; set; }
        public DbSet<Models.Relationship.OrganizationRecipes> OrganizationRecipes { get; set; }
        public DbSet<Models.Relationship.RecipeSiteImages> RecipeSiteImages { get; set; }
        #endregion

    }
}
