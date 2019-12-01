using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eyon.Models;
using Eyon.Models.Relationship;

namespace Eyon.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>()
                .HasIndex(u => u.WikipediaURL)
                .IsUnique();

            modelBuilder.Entity<CommunityCookbooks>()
                .HasKey(bc => new { bc.CookbookId, bc.CommunityId });
            modelBuilder.Entity<CommunityCookbooks>()
                .HasOne(cc => cc.Community)
                .WithMany(cc => cc.CommunityCookbooks)
                .HasForeignKey(cc => cc.CommunityId);
            modelBuilder.Entity<Models.Relationship.CommunityCookbooks>()
                .HasOne(cc => cc.Cookbook)
                .WithMany(cc => cc.CommunityCookbooks)
                .HasForeignKey(cc => cc.CookbookId);           

            modelBuilder.Entity<CookbookCategories>()
                .HasKey(bc => new { bc.CookbookId, bc.CategoryId});
            modelBuilder.Entity<CookbookCategories>()
                .HasOne(cc => cc.Cookbook)
                .WithMany(cc => cc.CookbookCategories)
                .HasForeignKey(cc => cc.CookbookId);
            modelBuilder.Entity<CookbookCategories>()
                .HasOne(cc => cc.Category)
                .WithMany(cc => cc.CookbookCategories)
                .HasForeignKey(cc => cc.CategoryId);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Category> Category { get; set; }
        public DbSet<SiteImage> SiteImage { get; set; }
        public DbSet<Community> Community { get; set; }
        public DbSet<Cookbook> Cookbook { get; set; }
        //public DbSet<Organization> Organization { get; set; }
        //public DbSet<Recipe> Recipe { get; set; }

        #region Relationship Tables
        public DbSet<CommunityCookbooks> CommunityCookbooks { get; set; }
        public DbSet<CookbookCategories> CookbookCategories { get; set; }
        /*
        public DbSet<Models.Relationship.CommunityOrganizations> CommunityOrganizations { get; set; }
        public DbSet<Models.Relationship.CookbookRecipes> CookbookRecipes { get; set; }
        public DbSet<Models.Relationship.CookbookSiteImages> CookbookSiteImages { get; set; }
        public DbSet<Models.Relationship.OrganizationCookbooks> OrganizationCookbooks { get; set; }
        public DbSet<Models.Relationship.OrganizationRecipes> OrganizationRecipes { get; set; }
        public DbSet<Models.Relationship.RecipeSiteImages> RecipeSiteImages { get; set; }
        public DbSet<Models.Relationship.RecipeCategories> RecipeCategories { get; set; }
        */
        #endregion

    }
}
