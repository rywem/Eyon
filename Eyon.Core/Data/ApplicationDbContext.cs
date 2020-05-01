using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eyon.Models;
using Eyon.Models.Relationship;
using Eyon.Models.Enums;

namespace Eyon.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Default Values
            
            modelBuilder.Entity<Recipe>()
                .Property(r => r.CreationDateTime)
                .HasDefaultValue(DateTime.MinValue.ToUniversalTime());

            modelBuilder.Entity<Cookbook>()
                .Property(r => r.CreationDateTime)
                .HasDefaultValue(DateTime.MinValue.ToUniversalTime());

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Privacy)
                .HasDefaultValue(Privacy.Public);

            modelBuilder.Entity<Cookbook>()
                .Property(r => r.Privacy)
                .HasDefaultValue(Privacy.Public);

            modelBuilder.Entity<UserImage>()
                .Property(r => r.CreationDateTime)
                .HasDefaultValue(DateTime.MinValue.ToUniversalTime());

            #endregion

            #region Indexes
            // Indexes: https://stackoverflow.com/questions/18889218/unique-key-constraints-for-multiple-columns-in-entity-framework
            //modelBuilder.Entity<Community>()
            //    .HasIndex(u => u.WikipediaURL)
            //    .IsUnique();

            modelBuilder.Entity<Recipe>()
                .HasIndex(x => x.CreationDateTime)
                .IsClustered(false);
            modelBuilder.Entity<UserImage>()
                .HasIndex(x => x.CreationDateTime)
                .IsClustered(false);

            modelBuilder.Entity<WebReference>()
                .HasIndex(u => u.Url)
                .IsUnique();

            modelBuilder.Entity<PostalCode>()
                .HasIndex(p => new { p.Text, p.CountryId })
                .IsUnique();

            modelBuilder.Entity<Geocode>()
                .HasIndex(p => new { p.Latitude, p.Longitude })
                .IsUnique();

            modelBuilder.Entity<CommunityGeocode>()
                .HasIndex(p => new { p.CommunityId, p.GeocodeId })
                .IsUnique();

            modelBuilder.Entity<Topic>()
                .HasIndex(p => new { p.ObjectId, p.TopicType })
                .IsUnique();

            modelBuilder.Entity<Topic>()
                .HasIndex(p => p.Name)
                .IsClustered(false);

            modelBuilder.Entity<Feed>()
                .HasIndex(x => x.CreationDateTime)
                .IsClustered(false);

            modelBuilder.Entity<Feed>()
                .HasIndex(x => x.ModifiedDateTime)
                .IsClustered(false);

            modelBuilder.Entity<Feed>()
                .HasIndex(x => x.Privacy)
                .IsClustered(false);

            #endregion

            #region Relationships


            modelBuilder.Entity<CommunityCookbook>()
                .HasKey(bc => new { bc.CookbookId, bc.CommunityId });
            modelBuilder.Entity<CommunityCookbook>()
                .HasOne(cc => cc.Community)
                .WithMany(cc => cc.CommunityCookbooks)
                .HasForeignKey(cc => cc.CommunityId);
            modelBuilder.Entity<Models.Relationship.CommunityCookbook>()
                .HasOne(cc => cc.Cookbook)
                .WithMany(cc => cc.CommunityCookbook)
                .HasForeignKey(cc => cc.CookbookId);

            modelBuilder.Entity<CommunityWebReference>()
                .HasKey(bc => new { bc.WebReferenceId, bc.CommunityId });
            modelBuilder.Entity<CommunityWebReference>()
                .HasOne(cc => cc.Community)
                .WithMany(cc => cc.CommunityWebReference)
                .HasForeignKey(cc => cc.CommunityId);

            modelBuilder.Entity<CommunityPostalCode>()
                .HasKey(bc => new { bc.CommunityId, bc.PostalCodeId });
            modelBuilder.Entity<CommunityPostalCode>()
                .HasOne(cc => cc.Community)
                .WithMany(cc => cc.CommunityPostalCode)
                .HasForeignKey(cc => cc.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Models.Relationship.CommunityPostalCode>()
                .HasOne(cc => cc.PostalCode)
                .WithMany(cc => cc.CommunityPostalCode)
                .HasForeignKey(cc => cc.PostalCodeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommunityGeocode>()
                .HasKey(bc => new { bc.GeocodeId, bc.CommunityId });
            modelBuilder.Entity<CommunityGeocode>()
                .HasOne(cc => cc.Community)
                .WithMany(cc => cc.CommunityGeocode)
                .HasForeignKey(cc => cc.CommunityId);
            modelBuilder.Entity<CommunityGeocode>()
                .HasOne(cc => cc.Geocode)
                .WithMany(cc => cc.CommunityGeocode)
                .HasForeignKey(cc => cc.GeocodeId);

            modelBuilder.Entity<PostalCodeGeocode>()
                .HasKey(bc => new { bc.GeocodeId, bc.PostalCodeId });
            modelBuilder.Entity<PostalCodeGeocode>()
                .HasOne(cc => cc.PostalCode)
                .WithMany(cc => cc.PostalCodeGeocode)
                .HasForeignKey(cc => cc.PostalCodeId);
            modelBuilder.Entity<PostalCodeGeocode>()
                .HasOne(cc => cc.Geocode)
                .WithMany(cc => cc.PostalCodeGeocode)
                .HasForeignKey(cc => cc.GeocodeId);

            modelBuilder.Entity<CookbookCategory>()
                .HasKey(bc => new { bc.CookbookId, bc.CategoryId });
            modelBuilder.Entity<CookbookCategory>()
                .HasOne(cc => cc.Cookbook)
                .WithMany(cc => cc.CookbookCategory)
                .HasForeignKey(cc => cc.CookbookId);
            modelBuilder.Entity<CookbookCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(cc => cc.CookbookCategory)
                .HasForeignKey(cc => cc.CategoryId);
            
            modelBuilder.Entity<OrganizationCommunity>()
                .HasKey(c => new { c.OrganizationId, c.CommunityId });
            modelBuilder.Entity<OrganizationCommunity>()
                .HasOne(c => c.Community)
                .WithMany(c => c.OrganizationCommunity)
                .HasForeignKey(c => c.CommunityId);
            modelBuilder.Entity<OrganizationCommunity>()
                .HasOne(c => c.Organization)
                .WithMany(c => c.OrganizationCommunity)
                .HasForeignKey(c => c.OrganizationId);
            
            modelBuilder.Entity<OrganizationCookbook>()
                .HasKey(o => new { o.OrganizationId, o.CookbookId });
            modelBuilder.Entity<OrganizationCookbook>()
                .HasOne(o => o.Organization)
                .WithMany(o => o.OrganizationCookbook)
                .HasForeignKey(o => o.OrganizationId);
            modelBuilder.Entity<OrganizationCookbook>()
                .HasOne(o => o.Cookbook)
                .WithMany(o => o.OrganizationCookbook)
                .HasForeignKey(o => o.CookbookId);
            
            modelBuilder.Entity<CommunityState>()
                .HasKey(cc => new { cc.CommunityId, cc.StateId });
            modelBuilder.Entity<CommunityState>()
                .HasOne(cc => cc.Community)
                .WithOne(cc => cc.CommunityState)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CommunityState>()
                .HasOne(cc => cc.State)
                .WithMany(cc => cc.CommunityState)
                .HasForeignKey(cc => cc.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommunityRecipe>()
                .HasKey(c => new { c.CommunityId, c.RecipeId });
            modelBuilder.Entity<CommunityRecipe>()
                .HasOne(c => c.Recipe)
                .WithOne(c => c.CommunityRecipe)                
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CommunityRecipe>()
                .HasOne(c => c.Community)
                .WithMany(c => c.CommunityRecipe)
                .HasForeignKey(c => c.CommunityId);
                
            modelBuilder.Entity<RecipeUserImage>()
                .HasKey(c => new { c.RecipeId, c.UserImageId });
            modelBuilder.Entity<RecipeUserImage>()
                .HasOne(c => c.Recipe)
                .WithMany(c => c.RecipeUserImage)
                .HasForeignKey(c => c.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                .HasKey(c => new { c.RecipeId, c.CategoryId });
            modelBuilder.Entity<RecipeCategory>()
                .HasOne(c => c.Recipe)
                .WithMany(c => c.RecipeCategory)
                .HasForeignKey(c => c.RecipeId);
            modelBuilder.Entity<RecipeCategory>()
                .HasOne(c => c.Category)
                .WithMany(c => c.RecipeCategory)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<CookbookRecipe>()
                .HasKey(c => new { c.RecipeId, c.CookbookId });
            modelBuilder.Entity<CookbookRecipe>()
                .HasOne(c => c.Cookbook)
                .WithMany(c => c.CookbookRecipe)
                .HasForeignKey(c => c.CookbookId);
            modelBuilder.Entity<CookbookRecipe>()
                .HasOne(c => c.Recipe)
                .WithMany(c => c.CookbookRecipe)
                .HasForeignKey(c => c.RecipeId);

            #region feed relationship tables

            modelBuilder.Entity<FeedCommunity>()
                .HasKey(c => new { c.FeedId, c.CommunityId });
            modelBuilder.Entity<FeedCommunity>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedCommunity)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedCommunity>()
                .HasOne(c => c.Community)
                .WithMany(c => c.FeedCommunity)
                .HasForeignKey(c => c.CommunityId);

            modelBuilder.Entity<FeedState>()
                .HasKey(c => new { c.FeedId, c.StateId });
            modelBuilder.Entity<FeedState>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedState)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedState>()
                .HasOne(c => c.State)
                .WithMany(c => c.FeedState)
                .HasForeignKey(c => c.StateId);

            modelBuilder.Entity<FeedUserImage>()
                .HasKey(c => new { c.FeedId, c.UserImageId });
            modelBuilder.Entity<FeedUserImage>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedUserImage)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedUserImage>()
                .HasOne(c => c.UserImage)
                .WithMany(c => c.FeedUserImage)
                .HasForeignKey(c => c.UserImageId);

            modelBuilder.Entity<FeedTopic>()
                .HasKey(c => new { c.FeedId, c.TopicId });
            modelBuilder.Entity<FeedTopic>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedTopic)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedTopic>()
                .HasOne(c => c.Topic)
                .WithMany(c => c.FeedTopic)
                .HasForeignKey(c => c.TopicId);

            modelBuilder.Entity<FeedOrganization>()
                .HasKey(c => new { c.FeedId, c.OrganizationId });
            modelBuilder.Entity<FeedOrganization>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedOrganization)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedOrganization>()
                .HasOne(c => c.Organization)
                .WithMany(c => c.FeedOrganization)
                .HasForeignKey(c => c.OrganizationId);

            modelBuilder.Entity<FeedCategory>()
                .HasKey(c => new { c.FeedId, c.CategoryId });
            modelBuilder.Entity<FeedCategory>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedCategory)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedCategory>()
                .HasOne(c => c.Category)
                .WithMany(c => c.FeedCategory)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<FeedCountry>()
                .HasKey(c => new { c.FeedId, c.CountryId });
            modelBuilder.Entity<FeedCountry>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedCountry)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedCountry>()
                .HasOne(c => c.Country)
                .WithMany(c => c.FeedCountry)
                .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<FeedCookbook>()
                .HasKey(c => new { c.FeedId, c.CookbookId });
            modelBuilder.Entity<FeedCookbook>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedCookbook)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedCookbook>()
                .HasOne(c => c.Cookbook)
                .WithOne(c => c.FeedCookbook)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FeedRecipe>()
                .HasKey(c => new { c.FeedId, c.RecipeId });
            modelBuilder.Entity<FeedRecipe>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedRecipe)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedRecipe>()
                .HasOne(c => c.Recipe)
                .WithOne(c => c.FeedRecipe)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FeedProfile>()
                .HasKey(c => new { c.FeedId, c.ProfileId });
            modelBuilder.Entity<FeedProfile>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.FeedProfile)
                .HasForeignKey(c => c.FeedId);
            modelBuilder.Entity<FeedProfile>()
                .HasOne(c => c.Profile)
                .WithMany(c => c.FeedProfile)
                .HasForeignKey(c => c.ProfileId);

            #endregion

            #region ownership tables 
            modelBuilder.Entity<ApplicationUserRecipe>()
                .HasKey(c => new { c.ObjectId, c.ApplicationUserId});
            modelBuilder.Entity<ApplicationUserRecipe>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.ApplicationUserRecipe)
                .HasForeignKey(c => c.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserRecipe>()
                .HasOne(c => c.Recipe)
                .WithMany(c => c.ApplicationUserOwner)
                .HasForeignKey(c => c.ObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserProfile>()
                .HasKey(c => new { c.ObjectId, c.ApplicationUserId });
            modelBuilder.Entity<ApplicationUserProfile>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.ApplicationUserProfile)
                .HasForeignKey(c => c.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserProfile>()
                .HasOne(c => c.Profile)
                .WithMany(c => c.ApplicationUserOwner)
                .HasForeignKey(c => c.ObjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUserOrganization>()
                .HasKey(c => new { c.ObjectId, c.ApplicationUserId });
            modelBuilder.Entity<ApplicationUserOrganization>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.ApplicationUserOrganization)
                .HasForeignKey(c => c.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserOrganization>()
                .HasOne(c => c.Organization)
                .WithMany(c => c.ApplicationUserOwner)
                .HasForeignKey(c => c.ObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserCookbook>()
                .HasKey(c => new { c.ObjectId, c.ApplicationUserId });
            modelBuilder.Entity<ApplicationUserCookbook>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.ApplicationUserCookbook)
                .HasForeignKey(c => c.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserCookbook>()
                .HasOne(c => c.Cookbook)
                .WithMany(c => c.ApplicationUserOwner)
                .HasForeignKey(c => c.ObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserUserImage>()
                .HasKey(a => new { a.ObjectId, a.ApplicationUserId });
            modelBuilder.Entity<ApplicationUserUserImage>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.ApplicationUserUserImage)
                .HasForeignKey(c => c.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserUserImage>()
                .HasOne(c => c.UserImage)
                .WithMany(c => c.ApplicationUserOwner)
                .HasForeignKey(c => c.ObjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUserFeed>()
                .HasKey(c => new { c.ObjectId, c.ApplicationUserId });
            modelBuilder.Entity<ApplicationUserFeed>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.ApplicationUserFeed)
                .HasForeignKey(c => c.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserFeed>()
                .HasOne(c => c.Feed)
                .WithMany(c => c.ApplicationUserOwner)
                .HasForeignKey(c => c.ObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #endregion

            #region Seed Data

            #region Seed Categories

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Cookies", SiteImageId = 1 },
                new Category() { Id = 2, Name = "Holiday Cookies",  SiteImageId = 2 },
                new Category() { Id = 3, Name = "Dinner", SiteImageId = 3 },
                new Category() { Id = 4, Name = "Breakfast", SiteImageId = 4 },
                new Category() { Id = 5, Name = "Salads", SiteImageId = 5 },
                new Category() { Id = 6, Name = "Soups and Stews",  SiteImageId = 6 },
                new Category() { Id = 7, Name = "Poultry", SiteImageId = 7 }
                );

            #endregion

            #region Seed Location Data

            #region Country
            modelBuilder.Entity<Country>().HasData(
                new Models.Country() { Name = "ALBANIA", Code = "AL", Id = 1 },
                new Models.Country() { Name = "ALGERIA", Code = "DZ", Id = 2 },
                new Models.Country() { Name = "ANDORRA", Code = "AD", Id = 3 },
                new Models.Country() { Name = "ANGOLA", Code = "AO", Id = 4 },
                new Models.Country() { Name = "ANGUILLA", Code = "AI", Id = 5 },
                new Models.Country() { Name = "ANTIGUA & BARBUDA", Code = "AG", Id = 6 },
                new Models.Country() { Name = "ARGENTINA", Code = "AR", Id = 7 },
                new Models.Country() { Name = "ARMENIA", Code = "AM", Id = 8 },
                new Models.Country() { Name = "ARUBA", Code = "AW", Id = 9 },
                new Models.Country() { Name = "AUSTRALIA", Code = "AU", Id = 10 },
                new Models.Country() { Name = "AUSTRIA", Code = "AT", Id = 11 },
                new Models.Country() { Name = "AZERBAIJAN", Code = "AZ", Id = 12 },
                new Models.Country() { Name = "BAHAMAS", Code = "BS", Id = 13 },
                new Models.Country() { Name = "BAHRAIN", Code = "BH", Id = 14 },
                new Models.Country() { Name = "BARBADOS", Code = "BB", Id = 15 },
                new Models.Country() { Name = "BELARUS", Code = "BY", Id = 16 },
                new Models.Country() { Name = "BELGIUM", Code = "BE", Id = 17 },
                new Models.Country() { Name = "BELIZE", Code = "BZ", Id = 18 },
                new Models.Country() { Name = "BENIN", Code = "BJ", Id = 19 },
                new Models.Country() { Name = "BERMUDA", Code = "BM", Id = 20 },
                new Models.Country() { Name = "BHUTAN", Code = "BT", Id = 21 },
                new Models.Country() { Name = "BOLIVIA", Code = "BO", Id = 22 },
                new Models.Country() { Name = "BOSNIA & HERZEGOVINA", Code = "BA", Id = 23 },
                new Models.Country() { Name = "BOTSWANA", Code = "BW", Id = 24 },
                new Models.Country() { Name = "BRAZIL", Code = "BR", Id = 25 },
                new Models.Country() { Name = "BRITISH VIRGIN ISLANDS", Code = "VG", Id = 26 },
                new Models.Country() { Name = "BRUNEI", Code = "BN", Id = 27 },
                new Models.Country() { Name = "BULGARIA", Code = "BG", Id = 28 },
                new Models.Country() { Name = "BURKINA FASO", Code = "BF", Id = 29 },
                new Models.Country() { Name = "BURUNDI", Code = "BI", Id = 30 },
                new Models.Country() { Name = "CAMBODIA", Code = "KH", Id = 31 },
                new Models.Country() { Name = "CAMEROON", Code = "CM", Id = 32 },
                new Models.Country() { Name = "CANADA", Code = "CA", Id = 33 },
                new Models.Country() { Name = "CAPE VERDE", Code = "CV", Id = 34 },
                new Models.Country() { Name = "CAYMAN ISLANDS", Code = "KY", Id = 35 },
                new Models.Country() { Name = "CHAD", Code = "TD", Id = 36 },
                new Models.Country() { Name = "CHILE", Code = "CL", Id = 37 },
                new Models.Country() { Name = "CHINA", Code = "C2", Id = 38 },
                new Models.Country() { Name = "COLOMBIA", Code = "CO", Id = 39 },
                new Models.Country() { Name = "COMOROS", Code = "KM", Id = 40 },
                new Models.Country() { Name = "CONGO - BRAZZAVILLE", Code = "CG", Id = 41 },
                new Models.Country() { Name = "CONGO - KINSHASA", Code = "CD", Id = 42 },
                new Models.Country() { Name = "COOK ISLANDS", Code = "CK", Id = 43 },
                new Models.Country() { Name = "COSTA RICA", Code = "CR", Id = 44 },
                new Models.Country() { Name = "CÔTE D’IVOIRE", Code = "CI", Id = 45 },
                new Models.Country() { Name = "CROATIA", Code = "HR", Id = 46 },
                new Models.Country() { Name = "CYPRUS", Code = "CY", Id = 47 },
                new Models.Country() { Name = "CZECH REPUBLIC", Code = "CZ", Id = 48 },
                new Models.Country() { Name = "DENMARK", Code = "DK", Id = 49 },
                new Models.Country() { Name = "DJIBOUTI", Code = "DJ", Id = 50 },
                new Models.Country() { Name = "DOMINICA", Code = "DM", Id = 51 },
                new Models.Country() { Name = "DOMINICAN REPUBLIC", Code = "DO", Id = 52 },
                new Models.Country() { Name = "ECUADOR", Code = "EC", Id = 53 },
                new Models.Country() { Name = "EGYPT", Code = "EG", Id = 54 },
                new Models.Country() { Name = "EL SALVADOR", Code = "SV", Id = 55 },
                new Models.Country() { Name = "ERITREA", Code = "ER", Id = 56 },
                new Models.Country() { Name = "ESTONIA", Code = "EE", Id = 57 },
                new Models.Country() { Name = "ETHIOPIA", Code = "ET", Id = 58 },
                new Models.Country() { Name = "FALKLAND ISLANDS", Code = "FK", Id = 59 },
                new Models.Country() { Name = "FAROE ISLANDS", Code = "FO", Id = 60 },
                new Models.Country() { Name = "FIJI", Code = "FJ", Id = 61 },
                new Models.Country() { Name = "FINLAND", Code = "FI", Id = 62 },
                new Models.Country() { Name = "FRANCE", Code = "FR", Id = 63 },
                new Models.Country() { Name = "FRENCH GUIANA", Code = "GF", Id = 64 },
                new Models.Country() { Name = "FRENCH POLYNESIA", Code = "PF", Id = 65 },
                new Models.Country() { Name = "GABON", Code = "GA", Id = 66 },
                new Models.Country() { Name = "GAMBIA", Code = "GM", Id = 67 },
                new Models.Country() { Name = "GEORGIA", Code = "GE", Id = 68 },
                new Models.Country() { Name = "GERMANY", Code = "DE", Id = 69 },
                new Models.Country() { Name = "GIBRALTAR", Code = "GI", Id = 70 },
                new Models.Country() { Name = "GREECE", Code = "GR", Id = 71 },
                new Models.Country() { Name = "GREENLAND", Code = "GL", Id = 72 },
                new Models.Country() { Name = "GRENADA", Code = "GD", Id = 73 },
                new Models.Country() { Name = "GUADELOUPE", Code = "GP", Id = 74 },
                new Models.Country() { Name = "GUATEMALA", Code = "GT", Id = 75 },
                new Models.Country() { Name = "GUINEA", Code = "GN", Id = 76 },
                new Models.Country() { Name = "GUINEA-BISSAU", Code = "GW", Id = 77 },
                new Models.Country() { Name = "GUYANA", Code = "GY", Id = 78 },
                new Models.Country() { Name = "HONDURAS", Code = "HN", Id = 79 },
                new Models.Country() { Name = "HONG KONG SAR CHINA", Code = "HK", Id = 80 },
                new Models.Country() { Name = "HUNGARY", Code = "HU", Id = 81 },
                new Models.Country() { Name = "ICELAND", Code = "IS", Id = 82 },
                new Models.Country() { Name = "INDIA", Code = "IN", Id = 83 },
                new Models.Country() { Name = "INDONESIA", Code = "ID", Id = 84 },
                new Models.Country() { Name = "IRELAND", Code = "IE", Id = 85 },
                new Models.Country() { Name = "ISRAEL", Code = "IL", Id = 86 },
                new Models.Country() { Name = "ITALY", Code = "IT", Id = 87 },
                new Models.Country() { Name = "JAMAICA", Code = "JM", Id = 88 },
                new Models.Country() { Name = "JAPAN", Code = "JP", Id = 89 },
                new Models.Country() { Name = "JORDAN", Code = "JO", Id = 90 },
                new Models.Country() { Name = "KAZAKHSTAN", Code = "KZ", Id = 91 },
                new Models.Country() { Name = "KENYA", Code = "KE", Id = 92 },
                new Models.Country() { Name = "KIRIBATI", Code = "KI", Id = 93 },
                new Models.Country() { Name = "KUWAIT", Code = "KW", Id = 94 },
                new Models.Country() { Name = "KYRGYZSTAN", Code = "KG", Id = 95 },
                new Models.Country() { Name = "LAOS", Code = "LA", Id = 96 },
                new Models.Country() { Name = "LATVIA", Code = "LV", Id = 97 },
                new Models.Country() { Name = "LESOTHO", Code = "LS", Id = 98 },
                new Models.Country() { Name = "LIECHTENSTEIN", Code = "LI", Id = 99 },
                new Models.Country() { Name = "LITHUANIA", Code = "LT", Id = 100 },
                new Models.Country() { Name = "LUXEMBOURG", Code = "LU", Id = 101 },
                new Models.Country() { Name = "MACEDONIA", Code = "MK", Id = 102 },
                new Models.Country() { Name = "MADAGASCAR", Code = "MG", Id = 103 },
                new Models.Country() { Name = "MALAWI", Code = "MW", Id = 104 },
                new Models.Country() { Name = "MALAYSIA", Code = "MY", Id = 105 },
                new Models.Country() { Name = "MALDIVES", Code = "MV", Id = 106 },
                new Models.Country() { Name = "MALI", Code = "ML", Id = 107 },
                new Models.Country() { Name = "MALTA", Code = "MT", Id = 108 },
                new Models.Country() { Name = "MARSHALL ISLANDS", Code = "MH", Id = 109 },
                new Models.Country() { Name = "MARTINIQUE", Code = "MQ", Id = 110 },
                new Models.Country() { Name = "MAURITANIA", Code = "MR", Id = 111 },
                new Models.Country() { Name = "MAURITIUS", Code = "MU", Id = 112 },
                new Models.Country() { Name = "MAYOTTE", Code = "YT", Id = 113 },
                new Models.Country() { Name = "MEXICO", Code = "MX", Id = 114 },
                new Models.Country() { Name = "MICRONESIA", Code = "FM", Id = 115 },
                new Models.Country() { Name = "MOLDOVA", Code = "MD", Id = 116 },
                new Models.Country() { Name = "MONACO", Code = "MC", Id = 117 },
                new Models.Country() { Name = "MONGOLIA", Code = "MN", Id = 118 },
                new Models.Country() { Name = "MONTENEGRO", Code = "ME", Id = 119 },
                new Models.Country() { Name = "MONTSERRAT", Code = "MS", Id = 120 },
                new Models.Country() { Name = "MOROCCO", Code = "MA", Id = 121 },
                new Models.Country() { Name = "MOZAMBIQUE", Code = "MZ", Id = 122 },
                new Models.Country() { Name = "NAMIBIA", Code = "NA", Id = 123 },
                new Models.Country() { Name = "NAURU", Code = "NR", Id = 124 },
                new Models.Country() { Name = "NEPAL", Code = "NP", Id = 125 },
                new Models.Country() { Name = "NETHERLANDS", Code = "NL", Id = 126 },
                new Models.Country() { Name = "NEW CALEDONIA", Code = "NC", Id = 127 },
                new Models.Country() { Name = "NEW ZEALAND", Code = "NZ", Id = 128 },
                new Models.Country() { Name = "NICARAGUA", Code = "NI", Id = 129 },
                new Models.Country() { Name = "NIGER", Code = "NE", Id = 130 },
                new Models.Country() { Name = "NIGERIA", Code = "NG", Id = 131 },
                new Models.Country() { Name = "NIUE", Code = "NU", Id = 132 },
                new Models.Country() { Name = "NORFOLK ISLAND", Code = "NF", Id = 133 },
                new Models.Country() { Name = "NORWAY", Code = "NO", Id = 134 },
                new Models.Country() { Name = "OMAN", Code = "OM", Id = 135 },
                new Models.Country() { Name = "PALAU", Code = "PW", Id = 136 },
                new Models.Country() { Name = "PANAMA", Code = "PA", Id = 137 },
                new Models.Country() { Name = "PAPUA NEW GUINEA", Code = "PG", Id = 138 },
                new Models.Country() { Name = "PARAGUAY", Code = "PY", Id = 139 },
                new Models.Country() { Name = "PERU", Code = "PE", Id = 140 },
                new Models.Country() { Name = "PHILIPPINES", Code = "PH", Id = 141 },
                new Models.Country() { Name = "PITCAIRN ISLANDS", Code = "PN", Id = 142 },
                new Models.Country() { Name = "POLAND", Code = "PL", Id = 143 },
                new Models.Country() { Name = "PORTUGAL", Code = "PT", Id = 144 },
                new Models.Country() { Name = "QATAR", Code = "QA", Id = 145 },
                new Models.Country() { Name = "RÉUNION", Code = "RE", Id = 146 },
                new Models.Country() { Name = "ROMANIA", Code = "RO", Id = 147 },
                new Models.Country() { Name = "RUSSIA", Code = "RU", Id = 148 },
                new Models.Country() { Name = "RWANDA", Code = "RW", Id = 149 },
                new Models.Country() { Name = "SAMOA", Code = "WS", Id = 150 },
                new Models.Country() { Name = "SAN MARINO", Code = "SM", Id = 151 },
                new Models.Country() { Name = "SÃO TOMÉ & PRÍNCIPE", Code = "ST", Id = 152 },
                new Models.Country() { Name = "SAUDI ARABIA", Code = "SA", Id = 153 },
                new Models.Country() { Name = "SENEGAL", Code = "SN", Id = 154 },
                new Models.Country() { Name = "SERBIA", Code = "RS", Id = 155 },
                new Models.Country() { Name = "SEYCHELLES", Code = "SC", Id = 156 },
                new Models.Country() { Name = "SIERRA LEONE", Code = "SL", Id = 157 },
                new Models.Country() { Name = "SINGAPORE", Code = "SG", Id = 158 },
                new Models.Country() { Name = "SLOVAKIA", Code = "SK", Id = 159 },
                new Models.Country() { Name = "SLOVENIA", Code = "SI", Id = 160 },
                new Models.Country() { Name = "SOLOMON ISLANDS", Code = "SB", Id = 161 },
                new Models.Country() { Name = "SOMALIA", Code = "SO", Id = 162 },
                new Models.Country() { Name = "SOUTH AFRICA", Code = "ZA", Id = 163 },
                new Models.Country() { Name = "SOUTH KOREA", Code = "KR", Id = 164 },
                new Models.Country() { Name = "SPAIN", Code = "ES", Id = 165 },
                new Models.Country() { Name = "SRI LANKA", Code = "LK", Id = 166 },
                new Models.Country() { Name = "ST. HELENA", Code = "SH", Id = 167 },
                new Models.Country() { Name = "ST. KITTS & NEVIS", Code = "KN", Id = 168 },
                new Models.Country() { Name = "ST. LUCIA", Code = "LC", Id = 169 },
                new Models.Country() { Name = "ST. PIERRE & MIQUELON", Code = "PM", Id = 170 },
                new Models.Country() { Name = "ST. VINCENT & GRENADINES", Code = "VC", Id = 171 },
                new Models.Country() { Name = "SURINAME", Code = "SR", Id = 172 },
                new Models.Country() { Name = "SVALBARD & JAN MAYEN", Code = "SJ", Id = 173 },
                new Models.Country() { Name = "SWAZILAND", Code = "SZ", Id = 174 },
                new Models.Country() { Name = "SWEDEN", Code = "SE", Id = 175 },
                new Models.Country() { Name = "SWITZERLAND", Code = "CH", Id = 176 },
                new Models.Country() { Name = "TAIWAN", Code = "TW", Id = 177 },
                new Models.Country() { Name = "TAJIKISTAN", Code = "TJ", Id = 178 },
                new Models.Country() { Name = "TANZANIA", Code = "TZ", Id = 179 },
                new Models.Country() { Name = "THAILAND", Code = "TH", Id = 180 },
                new Models.Country() { Name = "TOGO", Code = "TG", Id = 181 },
                new Models.Country() { Name = "TONGA", Code = "TO", Id = 182 },
                new Models.Country() { Name = "TRINIDAD & TOBAGO", Code = "TT", Id = 183 },
                new Models.Country() { Name = "TUNISIA", Code = "TN", Id = 184 },
                new Models.Country() { Name = "TURKMENISTAN", Code = "TM", Id = 185 },
                new Models.Country() { Name = "TURKS & CAICOS ISLANDS", Code = "TC", Id = 186 },
                new Models.Country() { Name = "TUVALU", Code = "TV", Id = 187 },
                new Models.Country() { Name = "UGANDA", Code = "UG", Id = 188 },
                new Models.Country() { Name = "UKRAINE", Code = "UA", Id = 189 },
                new Models.Country() { Name = "UNITED ARAB EMIRATES", Code = "AE", Id = 190 },
                new Models.Country() { Name = "UNITED KINGDOM", Code = "GB", Id = 191 },
                new Models.Country() { Name = "UNITED STATES", Code = "US", Id = 192 },
                new Models.Country() { Name = "URUGUAY", Code = "UY", Id = 193 },
                new Models.Country() { Name = "VANUATU", Code = "VU", Id = 194 },
                new Models.Country() { Name = "VATICAN CITY", Code = "VA", Id = 195 },
                new Models.Country() { Name = "VENEZUELA", Code = "VE", Id = 196 },
                new Models.Country() { Name = "VIETNAM", Code = "VN", Id = 197 },
                new Models.Country() { Name = "WALLIS & FUTUNA", Code = "WF", Id = 198 },
                new Models.Country() { Name = "YEMEN", Code = "YE", Id = 199 },
                new Models.Country() { Name = "ZAMBIA", Code = "ZM", Id = 200 },
                new Models.Country() { Name = "ZIMBABWE", Code = "ZW", Id = 201 });

            #endregion

            #region State/Province
            modelBuilder.Entity<State>().HasData(
                new Models.State() { Name = "Buenos Aires (Ciudad)", Code = "CIUDAD AUTÓNOMA DE BUENOS AIRES", Id = 1, CountryId = 7, LocalName = "Buenos Aires (Ciudad)", Type = "Province" },
                new Models.State() { Name = "Buenos Aires (Provincia)", Code = "BUENOS AIRES", Id = 2, CountryId = 7, LocalName = "Buenos Aires (Provincia)", Type = "Province" },
                new Models.State() { Name = "Catamarca", Code = "CATAMARCA", Id = 3, CountryId = 7, LocalName = "Catamarca", Type = "Province" },
                new Models.State() { Name = "Chaco", Code = "CHACO", Id = 4, CountryId = 7, LocalName = "Chaco", Type = "Province" },
                new Models.State() { Name = "Chubut", Code = "CHUBUT", Id = 5, CountryId = 7, LocalName = "Chubut", Type = "Province" },
                new Models.State() { Name = "Corrientes", Code = "CORRIENTES", Id = 6, CountryId = 7, LocalName = "Corrientes", Type = "Province" },
                new Models.State() { Name = "Córdoba", Code = "CÓRDOBA", Id = 7, CountryId = 7, LocalName = "Córdoba", Type = "Province" },
                new Models.State() { Name = "Entre Ríos", Code = "ENTRE RÍOS", Id = 8, CountryId = 7, LocalName = "Entre Ríos", Type = "Province" },
                new Models.State() { Name = "Formosa", Code = "FORMOSA", Id = 9, CountryId = 7, LocalName = "Formosa", Type = "Province" },
                new Models.State() { Name = "Jujuy", Code = "JUJUY", Id = 10, CountryId = 7, LocalName = "Jujuy", Type = "Province" },
                new Models.State() { Name = "La Pampa", Code = "LA PAMPA", Id = 11, CountryId = 7, LocalName = "La Pampa", Type = "Province" },
                new Models.State() { Name = "La Rioja", Code = "LA RIOJA", Id = 12, CountryId = 7, LocalName = "La Rioja", Type = "Province" },
                new Models.State() { Name = "Mendoza", Code = "MENDOZA", Id = 13, CountryId = 7, LocalName = "Mendoza", Type = "Province" },
                new Models.State() { Name = "Misiones", Code = "MISIONES", Id = 14, CountryId = 7, LocalName = "Misiones", Type = "Province" },
                new Models.State() { Name = "Neuquén", Code = "NEUQUÉN", Id = 15, CountryId = 7, LocalName = "Neuquén", Type = "Province" },
                new Models.State() { Name = "Río Negro", Code = "RÍO NEGRO", Id = 16, CountryId = 7, LocalName = "Río Negro", Type = "Province" },
                new Models.State() { Name = "Salta", Code = "SALTA", Id = 17, CountryId = 7, LocalName = "Salta", Type = "Province" },
                new Models.State() { Name = "San Juan", Code = "SAN JUAN", Id = 18, CountryId = 7, LocalName = "San Juan", Type = "Province" },
                new Models.State() { Name = "San Luis", Code = "SAN LUIS", Id = 19, CountryId = 7, LocalName = "San Luis", Type = "Province" },
                new Models.State() { Name = "Santa Cruz", Code = "SANTA CRUZ", Id = 20, CountryId = 7, LocalName = "Santa Cruz", Type = "Province" },
                new Models.State() { Name = "Santa Fe", Code = "SANTA FE", Id = 21, CountryId = 7, LocalName = "Santa Fe", Type = "Province" },
                new Models.State() { Name = "Santiago del Estero", Code = "SANTIAGO DEL ESTERO", Id = 22, CountryId = 7, LocalName = "Santiago del Estero", Type = "Province" },
                new Models.State() { Name = "Tierra del Fuego", Code = "TIERRA DEL FUEGO", Id = 23, CountryId = 7, LocalName = "Tierra del Fuego", Type = "Province" },
                new Models.State() { Name = "Tucumán", Code = "TUCUMÁN", Id = 24, CountryId = 7, LocalName = "Tucumán", Type = "Province" },
                new Models.State() { Name = "Acre", Code = "AC", Id = 25, CountryId = 25, LocalName = "Acre", Type = "State" },
                new Models.State() { Name = "Alagoas", Code = "AL", Id = 26, CountryId = 25, LocalName = "Alagoas", Type = "State" },
                new Models.State() { Name = "Amapá", Code = "AP", Id = 27, CountryId = 25, LocalName = "Amapá", Type = "State" },
                new Models.State() { Name = "Amazonas", Code = "AM", Id = 28, CountryId = 25, LocalName = "Amazonas", Type = "State" },
                new Models.State() { Name = "Bahia", Code = "BA", Id = 29, CountryId = 25, LocalName = "Bahia", Type = "State" },
                new Models.State() { Name = "Ceará", Code = "CE", Id = 30, CountryId = 25, LocalName = "Ceará", Type = "State" },
                new Models.State() { Name = "Distrito Federal", Code = "DF", Id = 31, CountryId = 25, LocalName = "Distrito Federal", Type = "State" },
                new Models.State() { Name = "Espírito Santo", Code = "ES", Id = 32, CountryId = 25, LocalName = "Espírito Santo", Type = "State" },
                new Models.State() { Name = "Goiás", Code = "GO", Id = 33, CountryId = 25, LocalName = "Goiás", Type = "State" },
                new Models.State() { Name = "Maranhão", Code = "MA", Id = 34, CountryId = 25, LocalName = "Maranhão", Type = "State" },
                new Models.State() { Name = "Mato Grosso", Code = "MT", Id = 35, CountryId = 25, LocalName = "Mato Grosso", Type = "State" },
                new Models.State() { Name = "Mato Grosso do Sul", Code = "MS", Id = 36, CountryId = 25, LocalName = "Mato Grosso do Sul", Type = "State" },
                new Models.State() { Name = "Minas Gerais", Code = "MG", Id = 37, CountryId = 25, LocalName = "Minas Gerais", Type = "State" },
                new Models.State() { Name = "Paraná", Code = "PR", Id = 38, CountryId = 25, LocalName = "Paraná", Type = "State" },
                new Models.State() { Name = "Paraíba", Code = "PB", Id = 39, CountryId = 25, LocalName = "Paraíba", Type = "State" },
                new Models.State() { Name = "Pará", Code = "PA", Id = 40, CountryId = 25, LocalName = "Pará", Type = "State" },
                new Models.State() { Name = "Pernambuco", Code = "PE", Id = 41, CountryId = 25, LocalName = "Pernambuco", Type = "State" },
                new Models.State() { Name = "Piauí", Code = "PI", Id = 42, CountryId = 25, LocalName = "Piauí", Type = "State" },
                new Models.State() { Name = "Rio Grande do Norte", Code = "RN", Id = 43, CountryId = 25, LocalName = "Rio Grande do Norte", Type = "State" },
                new Models.State() { Name = "Rio Grande do Sul", Code = "RS", Id = 44, CountryId = 25, LocalName = "Rio Grande do Sul", Type = "State" },
                new Models.State() { Name = "Rio de Janeiro", Code = "RJ", Id = 45, CountryId = 25, LocalName = "Rio de Janeiro", Type = "State" },
                new Models.State() { Name = "Rondônia", Code = "RO", Id = 46, CountryId = 25, LocalName = "Rondônia", Type = "State" },
                new Models.State() { Name = "Roraima", Code = "RR", Id = 47, CountryId = 25, LocalName = "Roraima", Type = "State" },
                new Models.State() { Name = "Santa Catarina", Code = "SC", Id = 48, CountryId = 25, LocalName = "Santa Catarina", Type = "State" },
                new Models.State() { Name = "Sergipe", Code = "SE", Id = 49, CountryId = 25, LocalName = "Sergipe", Type = "State" },
                new Models.State() { Name = "São Paulo", Code = "SP", Id = 50, CountryId = 25, LocalName = "São Paulo", Type = "State" },
                new Models.State() { Name = "Tocantins", Code = "TO", Id = 51, CountryId = 25, LocalName = "Tocantins", Type = "State" },
                new Models.State() { Name = "Alberta", Code = "AB", Id = 52, CountryId = 33, LocalName = "Alberta", Type = "Province" },
                new Models.State() { Name = "British Columbia", Code = "BC", Id = 53, CountryId = 33, LocalName = "British Columbia", Type = "Province" },
                new Models.State() { Name = "Manitoba", Code = "MB", Id = 54, CountryId = 33, LocalName = "Manitoba", Type = "Province" },
                new Models.State() { Name = "New Brunswick", Code = "NB", Id = 55, CountryId = 33, LocalName = "New Brunswick", Type = "Province" },
                new Models.State() { Name = "Newfoundland and Labrador", Code = "NL", Id = 56, CountryId = 33, LocalName = "Newfoundland and Labrador", Type = "Province" },
                new Models.State() { Name = "Northwest Territories", Code = "NT", Id = 57, CountryId = 33, LocalName = "Northwest Territories", Type = "Province" },
                new Models.State() { Name = "Nova Scotia", Code = "NS", Id = 58, CountryId = 33, LocalName = "Nova Scotia", Type = "Province" },
                new Models.State() { Name = "Nunavut", Code = "NU", Id = 59, CountryId = 33, LocalName = "Nunavut", Type = "Province" },
                new Models.State() { Name = "Ontario", Code = "ON", Id = 60, CountryId = 33, LocalName = "Ontario", Type = "Province" },
                new Models.State() { Name = "Prince Edward Island", Code = "PE", Id = 61, CountryId = 33, LocalName = "Prince Edward Island", Type = "Province" },
                new Models.State() { Name = "Quebec", Code = "QC", Id = 62, CountryId = 33, LocalName = "Quebec", Type = "Province" },
                new Models.State() { Name = "Saskatchewan", Code = "SK", Id = 63, CountryId = 33, LocalName = "Saskatchewan", Type = "Province" },
                new Models.State() { Name = "Yukon", Code = "YT", Id = 64, CountryId = 33, LocalName = "Yukon", Type = "Province" },
                new Models.State() { Name = "Anhui Sheng", Code = "CN-AH", Id = 65, CountryId = 38, LocalName = "安徽省 (Ānhuī Shěng)", Type = "Province" },
                new Models.State() { Name = "Beijing Shi", Code = "CN-BJ", Id = 66, CountryId = 38, LocalName = "北京市 (Běijīng Shì)", Type = "Municipality" },
                new Models.State() { Name = "Chongqing Shi", Code = "CN-CQ", Id = 67, CountryId = 38, LocalName = "重庆市 (Chóngqìng Shì)", Type = "Municipality" },
                new Models.State() { Name = "Fujian Sheng", Code = "CN-FJ", Id = 68, CountryId = 38, LocalName = "福建省 (Fújiàn Shěng)", Type = "Province" },
                new Models.State() { Name = "Guangdong Sheng", Code = "CN-GD", Id = 69, CountryId = 38, LocalName = "广东省 (Guǎngdōng Shěng)", Type = "Province" },
                new Models.State() { Name = "Gansu Sheng", Code = "CN-GS", Id = 70, CountryId = 38, LocalName = "甘肃省 (Gānsù Shěng)", Type = "Province" },
                new Models.State() { Name = "Guangxi Zhuangzu Zizhiqu", Code = "CN-GX", Id = 71, CountryId = 38, LocalName = "广西壮族自治区 (Guǎngxī Zhuàngzú Zìzhìqū)", Type = "Autonomous region" },
                new Models.State() { Name = "Guizhou Sheng", Code = "CN-GZ", Id = 72, CountryId = 38, LocalName = "贵州省 (Guìzhōu Shěng)", Type = "Province" },
                new Models.State() { Name = "Henan Sheng", Code = "CN-HA", Id = 73, CountryId = 38, LocalName = "河南省 (Hénán Shěng)", Type = "Province" },
                new Models.State() { Name = "Hubei Sheng", Code = "CN-HB", Id = 74, CountryId = 38, LocalName = "湖北省 (Húběi Shěng)", Type = "Province" },
                new Models.State() { Name = "Hebei Sheng", Code = "CN-HE", Id = 75, CountryId = 38, LocalName = "河北省 (Héběi Shěng)", Type = "Province" },
                new Models.State() { Name = "Hainan Sheng", Code = "CN-HI", Id = 76, CountryId = 38, LocalName = "海南省 (Hǎinán Shěng)", Type = "Province" },
                new Models.State() { Name = "Hong Kong SAR (en)", Code = "CN-HK", Id = 77, CountryId = 38, LocalName = "香港特别行政区 (Xiānggǎng Tèbiéxíngzhèngqū)", Type = "Special administrative region" },
                new Models.State() { Name = "Xianggang Tebiexingzhengqu (zh)", Code = "", Id = 78, CountryId = 38, LocalName = "Xianggang Tebiexingzhengqu (zh)", Type = "Province" },
                new Models.State() { Name = "Heilongjiang Sheng", Code = "CN-HL", Id = 79, CountryId = 38, LocalName = "黑龙江省 (Hēilóngjiāng Shěng)", Type = "Province" },
                new Models.State() { Name = "Hunan Sheng", Code = "CN-HN", Id = 80, CountryId = 38, LocalName = "湖南省 (Húnán Shěng)", Type = "Province" },
                new Models.State() { Name = "Jilin Sheng", Code = "CN-JL", Id = 81, CountryId = 38, LocalName = "吉林省 (Jílín Shěng)", Type = "Province" },
                new Models.State() { Name = "Jiangsu Sheng", Code = "CN-JS", Id = 82, CountryId = 38, LocalName = "江苏省 (Jiāngsū Shěng)", Type = "Province" },
                new Models.State() { Name = "Jiangxi Sheng", Code = "CN-JX", Id = 83, CountryId = 38, LocalName = "江西省 (Jiāngxī Shěng)", Type = "Province" },
                new Models.State() { Name = "Liaoning Sheng", Code = "CN-LN", Id = 84, CountryId = 38, LocalName = "辽宁省 (Liáoníng Shěng)", Type = "Province" },
                new Models.State() { Name = "Macao SAR (en)", Code = "CN-MO", Id = 85, CountryId = 38, LocalName = "澳门特别行政区 (Àomén Tèbiéxíngzhèngqū)", Type = "Special administrative region" },
                new Models.State() { Name = "Macau SAR (pt)", Code = "", Id = 86, CountryId = 38, LocalName = "Macau SAR (pt)", Type = "Province" },
                new Models.State() { Name = "Aomen Tebiexingzhengqu (zh)", Code = "", Id = 87, CountryId = 38, LocalName = "Aomen Tebiexingzhengqu (zh)", Type = "Province" },
                new Models.State() { Name = "Nei Mongol Zizhiqu (mn)", Code = "CN-NM", Id = 88, CountryId = 38, LocalName = "内蒙古自治区 (Nèi Ménggǔ Zìzhìqū)", Type = "Autonomous region" },
                new Models.State() { Name = "Ningxia Huizu Zizhiqu", Code = "CN-NX", Id = 89, CountryId = 38, LocalName = "宁夏回族自治区 (Níngxià Huízú Zìzhìqū)", Type = "Autonomous region" },
                new Models.State() { Name = "Qinghai Sheng", Code = "CN-QH", Id = 90, CountryId = 38, LocalName = "青海省 (Qīnghǎi Shěng)", Type = "Province" },
                new Models.State() { Name = "Sichuan Sheng", Code = "CN-SC", Id = 91, CountryId = 38, LocalName = "四川省 (Sìchuān Shěng)", Type = "Province" },
                new Models.State() { Name = "Shandong Sheng", Code = "CN-SD", Id = 92, CountryId = 38, LocalName = "山东省 (Shāndōng Shěng)", Type = "Province" },
                new Models.State() { Name = "Shanghai Shi", Code = "CN-SH", Id = 93, CountryId = 38, LocalName = "上海市 (Shànghǎi Shì)", Type = "Municipality" },
                new Models.State() { Name = "Shaanxi Sheng", Code = "CN-SN", Id = 94, CountryId = 38, LocalName = "陕西省 (Shǎnxī Shěng)", Type = "Province" },
                new Models.State() { Name = "Shanxi Sheng", Code = "CN-SX", Id = 95, CountryId = 38, LocalName = "山西省 (Shānxī Shěng)", Type = "Province" },
                new Models.State() { Name = "Tianjin Shi", Code = "CN-TJ", Id = 96, CountryId = 38, LocalName = "天津市 (Tiānjīn Shì)", Type = "Municipality" },
                new Models.State() { Name = "Taiwan Sheng", Code = "CN-TW", Id = 97, CountryId = 38, LocalName = "台湾省 (Táiwān Shěng)", Type = "Province" },
                new Models.State() { Name = "Xinjiang Uygur Zizhiqu", Code = "CN-XJ", Id = 98, CountryId = 38, LocalName = "新疆维吾尔自治区 (Xīnjiāng Wéiwú'ěr Zìzhìqū)", Type = "Autonomous region" },
                new Models.State() { Name = "Xizang Zizhiqu", Code = "CN-XZ", Id = 99, CountryId = 38, LocalName = "西藏自治区 (Xīzàng Zìzhìqū)", Type = "Autonomous region" },
                new Models.State() { Name = "Yunnan Sheng", Code = "CN-YN", Id = 100, CountryId = 38, LocalName = "云南省 (Yúnnán Shěng)", Type = "Province" },
                new Models.State() { Name = "Zhejiang Sheng", Code = "CN-ZJ", Id = 101, CountryId = 38, LocalName = "浙江省 (Zhèjiāng Shěng)", Type = "Province" },
                new Models.State() { Name = "Andaman and Nicobar Islands", Code = "Andaman and Nicobar Islands", Id = 102, CountryId = 83, LocalName = "Andaman and Nicobar Islands", Type = "State" },
                new Models.State() { Name = "Andhra Pradesh", Code = "Andhra Pradesh", Id = 103, CountryId = 83, LocalName = "Andhra Pradesh", Type = "State" },
                new Models.State() { Name = "Army Post Office", Code = "APO", Id = 104, CountryId = 83, LocalName = "Army Post Office", Type = "State" },
                new Models.State() { Name = "Arunachal Pradesh", Code = "Arunachal Pradesh", Id = 105, CountryId = 83, LocalName = "Arunachal Pradesh", Type = "State" },
                new Models.State() { Name = "Assam", Code = "Assam", Id = 106, CountryId = 83, LocalName = "Assam", Type = "State" },
                new Models.State() { Name = "Bihar", Code = "Bihar", Id = 107, CountryId = 83, LocalName = "Bihar", Type = "State" },
                new Models.State() { Name = "Chandigarh", Code = "Chandigarh", Id = 108, CountryId = 83, LocalName = "Chandigarh", Type = "State" },
                new Models.State() { Name = "Chhattisgarh", Code = "Chhattisgarh", Id = 109, CountryId = 83, LocalName = "Chhattisgarh", Type = "State" },
                new Models.State() { Name = "Dadra and Nagar Haveli", Code = "Dadra and Nagar Haveli", Id = 110, CountryId = 83, LocalName = "Dadra and Nagar Haveli", Type = "State" },
                new Models.State() { Name = "Daman and Diu", Code = "Daman and Diu", Id = 111, CountryId = 83, LocalName = "Daman and Diu", Type = "State" },
                new Models.State() { Name = "Delhi", Code = "Delhi (NCT)", Id = 112, CountryId = 83, LocalName = "Delhi", Type = "State" },
                new Models.State() { Name = "Goa", Code = "Goa", Id = 113, CountryId = 83, LocalName = "Goa", Type = "State" },
                new Models.State() { Name = "Gujarat", Code = "Gujarat", Id = 114, CountryId = 83, LocalName = "Gujarat", Type = "State" },
                new Models.State() { Name = "Haryana", Code = "Haryana", Id = 115, CountryId = 83, LocalName = "Haryana", Type = "State" },
                new Models.State() { Name = "Himachal Pradesh", Code = "Himachal Pradesh", Id = 116, CountryId = 83, LocalName = "Himachal Pradesh", Type = "State" },
                new Models.State() { Name = "Jammu and Kashmir", Code = "Jammu and Kashmir", Id = 117, CountryId = 83, LocalName = "Jammu and Kashmir", Type = "State" },
                new Models.State() { Name = "Jharkhand", Code = "Jharkhand", Id = 118, CountryId = 83, LocalName = "Jharkhand", Type = "State" },
                new Models.State() { Name = "Karnataka", Code = "Karnataka", Id = 119, CountryId = 83, LocalName = "Karnataka", Type = "State" },
                new Models.State() { Name = "Kerala", Code = "Kerala", Id = 120, CountryId = 83, LocalName = "Kerala", Type = "State" },
                new Models.State() { Name = "Lakshadweep", Code = "Lakshadweep", Id = 121, CountryId = 83, LocalName = "Lakshadweep", Type = "State" },
                new Models.State() { Name = "Madhya Pradesh", Code = "Madhya Pradesh", Id = 122, CountryId = 83, LocalName = "Madhya Pradesh", Type = "State" },
                new Models.State() { Name = "Maharashtra", Code = "Maharashtra", Id = 123, CountryId = 83, LocalName = "Maharashtra", Type = "State" },
                new Models.State() { Name = "Manipur", Code = "Manipur", Id = 124, CountryId = 83, LocalName = "Manipur", Type = "State" },
                new Models.State() { Name = "Meghalaya", Code = "Meghalaya", Id = 125, CountryId = 83, LocalName = "Meghalaya", Type = "State" },
                new Models.State() { Name = "Mizoram", Code = "Mizoram", Id = 126, CountryId = 83, LocalName = "Mizoram", Type = "State" },
                new Models.State() { Name = "Nagaland", Code = "Nagaland", Id = 127, CountryId = 83, LocalName = "Nagaland", Type = "State" },
                new Models.State() { Name = "Odisha", Code = "Odisha", Id = 128, CountryId = 83, LocalName = "Odisha", Type = "State" },
                new Models.State() { Name = "Puducherry", Code = "Puducherry", Id = 129, CountryId = 83, LocalName = "Puducherry", Type = "State" },
                new Models.State() { Name = "Punjab", Code = "Punjab", Id = 130, CountryId = 83, LocalName = "Punjab", Type = "State" },
                new Models.State() { Name = "Rajasthan", Code = "Rajasthan", Id = 131, CountryId = 83, LocalName = "Rajasthan", Type = "State" },
                new Models.State() { Name = "Sikkim", Code = "Sikkim", Id = 132, CountryId = 83, LocalName = "Sikkim", Type = "State" },
                new Models.State() { Name = "Tamil Nadu", Code = "Tamil Nadu", Id = 133, CountryId = 83, LocalName = "Tamil Nadu", Type = "State" },
                new Models.State() { Name = "Telangana", Code = "Telangana", Id = 134, CountryId = 83, LocalName = "Telangana", Type = "State" },
                new Models.State() { Name = "Tripura", Code = "Tripura", Id = 135, CountryId = 83, LocalName = "Tripura", Type = "State" },
                new Models.State() { Name = "Uttar Pradesh", Code = "Uttar Pradesh", Id = 136, CountryId = 83, LocalName = "Uttar Pradesh", Type = "State" },
                new Models.State() { Name = "Uttarakhand", Code = "Uttarakhand", Id = 137, CountryId = 83, LocalName = "Uttarakhand", Type = "State" },
                new Models.State() { Name = "West Bengal", Code = "West Bengal", Id = 138, CountryId = 83, LocalName = "West Bengal", Type = "State" },
                new Models.State() { Name = "Bali", Code = "ID-BA", Id = 139, CountryId = 84, LocalName = "Bali", Type = "Province" },
                new Models.State() { Name = "Bangka Belitung", Code = "ID-BB", Id = 140, CountryId = 84, LocalName = "Bangka Belitung", Type = "Province" },
                new Models.State() { Name = "Banten", Code = "ID-BT", Id = 141, CountryId = 84, LocalName = "Banten", Type = "Province" },
                new Models.State() { Name = "Bengkulu", Code = "ID-BE", Id = 142, CountryId = 84, LocalName = "Bengkulu", Type = "Province" },
                new Models.State() { Name = "DI Yogyakarta", Code = "ID-YO", Id = 143, CountryId = 84, LocalName = "DI Yogyakarta", Type = "Province" },
                new Models.State() { Name = "DKI Jakarta", Code = "ID-JK", Id = 144, CountryId = 84, LocalName = "DKI Jakarta", Type = "Province" },
                new Models.State() { Name = "Gorontalo", Code = "ID-GO", Id = 145, CountryId = 84, LocalName = "Gorontalo", Type = "Province" },
                new Models.State() { Name = "Jambi", Code = "ID-JA", Id = 146, CountryId = 84, LocalName = "Jambi", Type = "Province" },
                new Models.State() { Name = "Jawa Barat", Code = "ID-JB", Id = 147, CountryId = 84, LocalName = "Jawa Barat", Type = "Province" },
                new Models.State() { Name = "Jawa Tengah", Code = "ID-JT", Id = 148, CountryId = 84, LocalName = "Jawa Tengah", Type = "Province" },
                new Models.State() { Name = "Jawa Timur", Code = "ID-JI", Id = 149, CountryId = 84, LocalName = "Jawa Timur", Type = "Province" },
                new Models.State() { Name = "Kalimantan Barat", Code = "ID-KB", Id = 150, CountryId = 84, LocalName = "Kalimantan Barat", Type = "Province" },
                new Models.State() { Name = "Kalimantan Selatan", Code = "ID-KS", Id = 151, CountryId = 84, LocalName = "Kalimantan Selatan", Type = "Province" },
                new Models.State() { Name = "Kalimantan Tengah", Code = "ID-KT", Id = 152, CountryId = 84, LocalName = "Kalimantan Tengah", Type = "Province" },
                new Models.State() { Name = "Kalimantan Timur", Code = "ID-KI", Id = 153, CountryId = 84, LocalName = "Kalimantan Timur", Type = "Province" },
                new Models.State() { Name = "Kalimantan Utara", Code = "ID-KU", Id = 154, CountryId = 84, LocalName = "Kalimantan Utara", Type = "Province" },
                new Models.State() { Name = "Kepulauan Riau", Code = "ID-KR", Id = 155, CountryId = 84, LocalName = "Kepulauan Riau", Type = "Province" },
                new Models.State() { Name = "Lampung", Code = "ID-LA", Id = 156, CountryId = 84, LocalName = "Lampung", Type = "Province" },
                new Models.State() { Name = "Maluku", Code = "ID-MA", Id = 157, CountryId = 84, LocalName = "Maluku", Type = "Province" },
                new Models.State() { Name = "Maluku Utara", Code = "ID-MU", Id = 158, CountryId = 84, LocalName = "Maluku Utara", Type = "Province" },
                new Models.State() { Name = "Nanggroe Aceh Darussalam", Code = "ID-AC", Id = 159, CountryId = 84, LocalName = "Nanggroe Aceh Darussalam", Type = "Province" },
                new Models.State() { Name = "Nusa Tenggara Barat", Code = "ID-NB", Id = 160, CountryId = 84, LocalName = "Nusa Tenggara Barat", Type = "Province" },
                new Models.State() { Name = "Nusa Tenggara Timur", Code = "ID-NT", Id = 161, CountryId = 84, LocalName = "Nusa Tenggara Timur", Type = "Province" },
                new Models.State() { Name = "Papua", Code = "ID-PA", Id = 162, CountryId = 84, LocalName = "Papua", Type = "Province" },
                new Models.State() { Name = "Papua Barat", Code = "ID-PB", Id = 163, CountryId = 84, LocalName = "Papua Barat", Type = "Province" },
                new Models.State() { Name = "Riau", Code = "ID-RI", Id = 164, CountryId = 84, LocalName = "Riau", Type = "Province" },
                new Models.State() { Name = "Sulawesi Barat", Code = "ID-SR", Id = 165, CountryId = 84, LocalName = "Sulawesi Barat", Type = "Province" },
                new Models.State() { Name = "Sulawesi Selatan", Code = "ID-SN", Id = 166, CountryId = 84, LocalName = "Sulawesi Selatan", Type = "Province" },
                new Models.State() { Name = "Sulawesi Tengah", Code = "ID-ST", Id = 167, CountryId = 84, LocalName = "Sulawesi Tengah", Type = "Province" },
                new Models.State() { Name = "Sulawesi Tenggara", Code = "ID-SG", Id = 168, CountryId = 84, LocalName = "Sulawesi Tenggara", Type = "Province" },
                new Models.State() { Name = "Sulawesi Utara", Code = "ID-SA", Id = 169, CountryId = 84, LocalName = "Sulawesi Utara", Type = "Province" },
                new Models.State() { Name = "Sumatera Barat", Code = "ID-SB", Id = 170, CountryId = 84, LocalName = "Sumatera Barat", Type = "Province" },
                new Models.State() { Name = "Sumatera Selatan", Code = "ID-SS", Id = 171, CountryId = 84, LocalName = "Sumatera Selatan", Type = "Province" },
                new Models.State() { Name = "Sumatera Utara", Code = "ID-SU", Id = 172, CountryId = 84, LocalName = "Sumatera Utara", Type = "Province" },
                new Models.State() { Name = "Agrigento", Code = "AG", Id = 173, CountryId = 87, LocalName = "Agrigento", Type = "Province" },
                new Models.State() { Name = "Alessandria", Code = "AL", Id = 174, CountryId = 87, LocalName = "Alessandria", Type = "Province" },
                new Models.State() { Name = "Ancona", Code = "AN", Id = 175, CountryId = 87, LocalName = "Ancona", Type = "Province" },
                new Models.State() { Name = "Aosta", Code = "AO", Id = 176, CountryId = 87, LocalName = "Aosta", Type = "Province" },
                new Models.State() { Name = "Arezzo", Code = "AR", Id = 177, CountryId = 87, LocalName = "Arezzo", Type = "Province" },
                new Models.State() { Name = "Ascoli Piceno", Code = "AP", Id = 178, CountryId = 87, LocalName = "Ascoli Piceno", Type = "Province" },
                new Models.State() { Name = "Asti", Code = "AT", Id = 179, CountryId = 87, LocalName = "Asti", Type = "Province" },
                new Models.State() { Name = "Avellino", Code = "AV", Id = 180, CountryId = 87, LocalName = "Avellino", Type = "Province" },
                new Models.State() { Name = "Bari", Code = "BA", Id = 181, CountryId = 87, LocalName = "Bari", Type = "Province" },
                new Models.State() { Name = "Barletta-Andria-Trani", Code = "BT", Id = 182, CountryId = 87, LocalName = "Barletta-Andria-Trani", Type = "Province" },
                new Models.State() { Name = "Belluno", Code = "BL", Id = 183, CountryId = 87, LocalName = "Belluno", Type = "Province" },
                new Models.State() { Name = "Benevento", Code = "BN", Id = 184, CountryId = 87, LocalName = "Benevento", Type = "Province" },
                new Models.State() { Name = "Bergamo", Code = "BG", Id = 185, CountryId = 87, LocalName = "Bergamo", Type = "Province" },
                new Models.State() { Name = "Biella", Code = "BI", Id = 186, CountryId = 87, LocalName = "Biella", Type = "Province" },
                new Models.State() { Name = "Bologna", Code = "BO", Id = 187, CountryId = 87, LocalName = "Bologna", Type = "Province" },
                new Models.State() { Name = "Bolzano", Code = "BZ", Id = 188, CountryId = 87, LocalName = "Bolzano", Type = "Province" },
                new Models.State() { Name = "Brescia", Code = "BS", Id = 189, CountryId = 87, LocalName = "Brescia", Type = "Province" },
                new Models.State() { Name = "Brindisi", Code = "BR", Id = 190, CountryId = 87, LocalName = "Brindisi", Type = "Province" },
                new Models.State() { Name = "Cagliari", Code = "CA", Id = 191, CountryId = 87, LocalName = "Cagliari", Type = "Province" },
                new Models.State() { Name = "Caltanissetta", Code = "CL", Id = 192, CountryId = 87, LocalName = "Caltanissetta", Type = "Province" },
                new Models.State() { Name = "Campobasso", Code = "CB", Id = 193, CountryId = 87, LocalName = "Campobasso", Type = "Province" },
                new Models.State() { Name = "Carbonia-Iglesias", Code = "CI", Id = 194, CountryId = 87, LocalName = "Carbonia-Iglesias", Type = "Province" },
                new Models.State() { Name = "Caserta", Code = "CE", Id = 195, CountryId = 87, LocalName = "Caserta", Type = "Province" },
                new Models.State() { Name = "Catania", Code = "CT", Id = 196, CountryId = 87, LocalName = "Catania", Type = "Province" },
                new Models.State() { Name = "Catanzaro", Code = "CZ", Id = 197, CountryId = 87, LocalName = "Catanzaro", Type = "Province" },
                new Models.State() { Name = "Chieti", Code = "CH", Id = 198, CountryId = 87, LocalName = "Chieti", Type = "Province" },
                new Models.State() { Name = "Como", Code = "CO", Id = 199, CountryId = 87, LocalName = "Como", Type = "Province" },
                new Models.State() { Name = "Cosenza", Code = "CS", Id = 200, CountryId = 87, LocalName = "Cosenza", Type = "Province" },
                new Models.State() { Name = "Cremona", Code = "CR", Id = 201, CountryId = 87, LocalName = "Cremona", Type = "Province" },
                new Models.State() { Name = "Crotone", Code = "KR", Id = 202, CountryId = 87, LocalName = "Crotone", Type = "Province" },
                new Models.State() { Name = "Cuneo", Code = "CN", Id = 203, CountryId = 87, LocalName = "Cuneo", Type = "Province" },
                new Models.State() { Name = "Enna", Code = "EN", Id = 204, CountryId = 87, LocalName = "Enna", Type = "Province" },
                new Models.State() { Name = "Fermo", Code = "FM", Id = 205, CountryId = 87, LocalName = "Fermo", Type = "Province" },
                new Models.State() { Name = "Ferrara", Code = "FE", Id = 206, CountryId = 87, LocalName = "Ferrara", Type = "Province" },
                new Models.State() { Name = "Firenze", Code = "FI", Id = 207, CountryId = 87, LocalName = "Firenze", Type = "Province" },
                new Models.State() { Name = "Foggia", Code = "FG", Id = 208, CountryId = 87, LocalName = "Foggia", Type = "Province" },
                new Models.State() { Name = "Forlì-Cesena", Code = "FC", Id = 209, CountryId = 87, LocalName = "Forlì-Cesena", Type = "Province" },
                new Models.State() { Name = "Frosinone", Code = "FR", Id = 210, CountryId = 87, LocalName = "Frosinone", Type = "Province" },
                new Models.State() { Name = "Genova", Code = "GE", Id = 211, CountryId = 87, LocalName = "Genova", Type = "Province" },
                new Models.State() { Name = "Gorizia", Code = "GO", Id = 212, CountryId = 87, LocalName = "Gorizia", Type = "Province" },
                new Models.State() { Name = "Grosseto", Code = "GR", Id = 213, CountryId = 87, LocalName = "Grosseto", Type = "Province" },
                new Models.State() { Name = "Imperia", Code = "IM", Id = 214, CountryId = 87, LocalName = "Imperia", Type = "Province" },
                new Models.State() { Name = "Isernia", Code = "IS", Id = 215, CountryId = 87, LocalName = "Isernia", Type = "Province" },
                new Models.State() { Name = "L'Aquila", Code = "AQ", Id = 216, CountryId = 87, LocalName = "L'Aquila", Type = "Province" },
                new Models.State() { Name = "La Spezia", Code = "SP", Id = 217, CountryId = 87, LocalName = "La Spezia", Type = "Province" },
                new Models.State() { Name = "Latina", Code = "LT", Id = 218, CountryId = 87, LocalName = "Latina", Type = "Province" },
                new Models.State() { Name = "Lecce", Code = "LE", Id = 219, CountryId = 87, LocalName = "Lecce", Type = "Province" },
                new Models.State() { Name = "Lecco", Code = "LC", Id = 220, CountryId = 87, LocalName = "Lecco", Type = "Province" },
                new Models.State() { Name = "Livorno", Code = "LI", Id = 221, CountryId = 87, LocalName = "Livorno", Type = "Province" },
                new Models.State() { Name = "Lodi", Code = "LO", Id = 222, CountryId = 87, LocalName = "Lodi", Type = "Province" },
                new Models.State() { Name = "Lucca", Code = "LU", Id = 223, CountryId = 87, LocalName = "Lucca", Type = "Province" },
                new Models.State() { Name = "Macerata", Code = "MC", Id = 224, CountryId = 87, LocalName = "Macerata", Type = "Province" },
                new Models.State() { Name = "Mantova", Code = "MN", Id = 225, CountryId = 87, LocalName = "Mantova", Type = "Province" },
                new Models.State() { Name = "Massa-Carrara", Code = "MS", Id = 226, CountryId = 87, LocalName = "Massa-Carrara", Type = "Province" },
                new Models.State() { Name = "Matera", Code = "MT", Id = 227, CountryId = 87, LocalName = "Matera", Type = "Province" },
                new Models.State() { Name = "Medio Campidano", Code = "VS", Id = 228, CountryId = 87, LocalName = "Medio Campidano", Type = "Province" },
                new Models.State() { Name = "Messina", Code = "ME", Id = 229, CountryId = 87, LocalName = "Messina", Type = "Province" },
                new Models.State() { Name = "Milano", Code = "MI", Id = 230, CountryId = 87, LocalName = "Milano", Type = "Province" },
                new Models.State() { Name = "Modena", Code = "MO", Id = 231, CountryId = 87, LocalName = "Modena", Type = "Province" },
                new Models.State() { Name = "Monza e della Brianza", Code = "MB", Id = 232, CountryId = 87, LocalName = "Monza e della Brianza", Type = "Province" },
                new Models.State() { Name = "Napoli", Code = "NA", Id = 233, CountryId = 87, LocalName = "Napoli", Type = "Province" },
                new Models.State() { Name = "Novara", Code = "NO", Id = 234, CountryId = 87, LocalName = "Novara", Type = "Province" },
                new Models.State() { Name = "Nuoro", Code = "NU", Id = 235, CountryId = 87, LocalName = "Nuoro", Type = "Province" },
                new Models.State() { Name = "Ogliastra", Code = "OG", Id = 236, CountryId = 87, LocalName = "Ogliastra", Type = "Province" },
                new Models.State() { Name = "Olbia-Tempio", Code = "OT", Id = 237, CountryId = 87, LocalName = "Olbia-Tempio", Type = "Province" },
                new Models.State() { Name = "Oristano", Code = "OR", Id = 238, CountryId = 87, LocalName = "Oristano", Type = "Province" },
                new Models.State() { Name = "Padova", Code = "PD", Id = 239, CountryId = 87, LocalName = "Padova", Type = "Province" },
                new Models.State() { Name = "Palermo", Code = "PA", Id = 240, CountryId = 87, LocalName = "Palermo", Type = "Province" },
                new Models.State() { Name = "Parma", Code = "PR", Id = 241, CountryId = 87, LocalName = "Parma", Type = "Province" },
                new Models.State() { Name = "Pavia", Code = "PV", Id = 242, CountryId = 87, LocalName = "Pavia", Type = "Province" },
                new Models.State() { Name = "Perugia", Code = "PG", Id = 243, CountryId = 87, LocalName = "Perugia", Type = "Province" },
                new Models.State() { Name = "Pesaro e Urbino", Code = "PU", Id = 244, CountryId = 87, LocalName = "Pesaro e Urbino", Type = "Province" },
                new Models.State() { Name = "Pescara", Code = "PE", Id = 245, CountryId = 87, LocalName = "Pescara", Type = "Province" },
                new Models.State() { Name = "Piacenza", Code = "PC", Id = 246, CountryId = 87, LocalName = "Piacenza", Type = "Province" },
                new Models.State() { Name = "Pisa", Code = "PI", Id = 247, CountryId = 87, LocalName = "Pisa", Type = "Province" },
                new Models.State() { Name = "Pistoia", Code = "PT", Id = 248, CountryId = 87, LocalName = "Pistoia", Type = "Province" },
                new Models.State() { Name = "Pordenone", Code = "PN", Id = 249, CountryId = 87, LocalName = "Pordenone", Type = "Province" },
                new Models.State() { Name = "Potenza", Code = "PZ", Id = 250, CountryId = 87, LocalName = "Potenza", Type = "Province" },
                new Models.State() { Name = "Prato", Code = "PO", Id = 251, CountryId = 87, LocalName = "Prato", Type = "Province" },
                new Models.State() { Name = "Ragusa", Code = "RG", Id = 252, CountryId = 87, LocalName = "Ragusa", Type = "Province" },
                new Models.State() { Name = "Ravenna", Code = "RA", Id = 253, CountryId = 87, LocalName = "Ravenna", Type = "Province" },
                new Models.State() { Name = "Reggio Calabria", Code = "RC", Id = 254, CountryId = 87, LocalName = "Reggio Calabria", Type = "Province" },
                new Models.State() { Name = "Reggio Emilia", Code = "RE", Id = 255, CountryId = 87, LocalName = "Reggio Emilia", Type = "Province" },
                new Models.State() { Name = "Rieti", Code = "RI", Id = 256, CountryId = 87, LocalName = "Rieti", Type = "Province" },
                new Models.State() { Name = "Rimini", Code = "RN", Id = 257, CountryId = 87, LocalName = "Rimini", Type = "Province" },
                new Models.State() { Name = "Roma", Code = "RM", Id = 258, CountryId = 87, LocalName = "Roma", Type = "Province" },
                new Models.State() { Name = "Rovigo", Code = "RO", Id = 259, CountryId = 87, LocalName = "Rovigo", Type = "Province" },
                new Models.State() { Name = "Salerno", Code = "SA", Id = 260, CountryId = 87, LocalName = "Salerno", Type = "Province" },
                new Models.State() { Name = "Sassari", Code = "SS", Id = 261, CountryId = 87, LocalName = "Sassari", Type = "Province" },
                new Models.State() { Name = "Savona", Code = "SV", Id = 262, CountryId = 87, LocalName = "Savona", Type = "Province" },
                new Models.State() { Name = "Siena", Code = "SI", Id = 263, CountryId = 87, LocalName = "Siena", Type = "Province" },
                new Models.State() { Name = "Siracusa", Code = "SR", Id = 264, CountryId = 87, LocalName = "Siracusa", Type = "Province" },
                new Models.State() { Name = "Sondrio", Code = "SO", Id = 265, CountryId = 87, LocalName = "Sondrio", Type = "Province" },
                new Models.State() { Name = "Taranto", Code = "TA", Id = 266, CountryId = 87, LocalName = "Taranto", Type = "Province" },
                new Models.State() { Name = "Teramo", Code = "TE", Id = 267, CountryId = 87, LocalName = "Teramo", Type = "Province" },
                new Models.State() { Name = "Terni", Code = "TR", Id = 268, CountryId = 87, LocalName = "Terni", Type = "Province" },
                new Models.State() { Name = "Torino", Code = "TO", Id = 269, CountryId = 87, LocalName = "Torino", Type = "Province" },
                new Models.State() { Name = "Trapani", Code = "TP", Id = 270, CountryId = 87, LocalName = "Trapani", Type = "Province" },
                new Models.State() { Name = "Trento", Code = "TN", Id = 271, CountryId = 87, LocalName = "Trento", Type = "Province" },
                new Models.State() { Name = "Treviso", Code = "TV", Id = 272, CountryId = 87, LocalName = "Treviso", Type = "Province" },
                new Models.State() { Name = "Trieste", Code = "TS", Id = 273, CountryId = 87, LocalName = "Trieste", Type = "Province" },
                new Models.State() { Name = "Udine", Code = "UD", Id = 274, CountryId = 87, LocalName = "Udine", Type = "Province" },
                new Models.State() { Name = "Varese", Code = "VA", Id = 275, CountryId = 87, LocalName = "Varese", Type = "Province" },
                new Models.State() { Name = "Venezia", Code = "VE", Id = 276, CountryId = 87, LocalName = "Venezia", Type = "Province" },
                new Models.State() { Name = "Verbano-Cusio-Ossola", Code = "VB", Id = 277, CountryId = 87, LocalName = "Verbano-Cusio-Ossola", Type = "Province" },
                new Models.State() { Name = "Vercelli", Code = "VC", Id = 278, CountryId = 87, LocalName = "Vercelli", Type = "Province" },
                new Models.State() { Name = "Verona", Code = "VR", Id = 279, CountryId = 87, LocalName = "Verona", Type = "Province" },
                new Models.State() { Name = "Vibo Valentia", Code = "VV", Id = 280, CountryId = 87, LocalName = "Vibo Valentia", Type = "Province" },
                new Models.State() { Name = "Vicenza", Code = "VI", Id = 281, CountryId = 87, LocalName = "Vicenza", Type = "Province" },
                new Models.State() { Name = "Viterbo", Code = "VT", Id = 282, CountryId = 87, LocalName = "Viterbo", Type = "Province" },
                new Models.State() { Name = "Aichi", Code = "AICHI-KEN", Id = 283, CountryId = 89, LocalName = "Aichi", Type = "Prefecture" },
                new Models.State() { Name = "Akita", Code = "AKITA-KEN", Id = 284, CountryId = 89, LocalName = "Akita", Type = "Prefecture" },
                new Models.State() { Name = "Aomori", Code = "AOMORI-KEN", Id = 285, CountryId = 89, LocalName = "Aomori", Type = "Prefecture" },
                new Models.State() { Name = "Chiba", Code = "CHIBA-KEN", Id = 286, CountryId = 89, LocalName = "Chiba", Type = "Prefecture" },
                new Models.State() { Name = "Ehime", Code = "EHIME-KEN", Id = 287, CountryId = 89, LocalName = "Ehime", Type = "Prefecture" },
                new Models.State() { Name = "Fukui", Code = "FUKUI-KEN", Id = 288, CountryId = 89, LocalName = "Fukui", Type = "Prefecture" },
                new Models.State() { Name = "Fukuoka", Code = "FUKUOKA-KEN", Id = 289, CountryId = 89, LocalName = "Fukuoka", Type = "Prefecture" },
                new Models.State() { Name = "Fukushima", Code = "FUKUSHIMA-KEN", Id = 290, CountryId = 89, LocalName = "Fukushima", Type = "Prefecture" },
                new Models.State() { Name = "Gifu", Code = "GIFU-KEN", Id = 291, CountryId = 89, LocalName = "Gifu", Type = "Prefecture" },
                new Models.State() { Name = "Gunma", Code = "GUNMA-KEN", Id = 292, CountryId = 89, LocalName = "Gunma", Type = "Prefecture" },
                new Models.State() { Name = "Hiroshima", Code = "HIROSHIMA-KEN", Id = 293, CountryId = 89, LocalName = "Hiroshima", Type = "Prefecture" },
                new Models.State() { Name = "Hokkaido", Code = "HOKKAIDO", Id = 294, CountryId = 89, LocalName = "Hokkaido", Type = "Prefecture" },
                new Models.State() { Name = "Hyogo", Code = "HYOGO-KEN", Id = 295, CountryId = 89, LocalName = "Hyogo", Type = "Prefecture" },
                new Models.State() { Name = "Ibaraki", Code = "IBARAKI-KEN", Id = 296, CountryId = 89, LocalName = "Ibaraki", Type = "Prefecture" },
                new Models.State() { Name = "Ishikawa", Code = "ISHIKAWA-KEN", Id = 297, CountryId = 89, LocalName = "Ishikawa", Type = "Prefecture" },
                new Models.State() { Name = "Iwate", Code = "IWATE-KEN", Id = 298, CountryId = 89, LocalName = "Iwate", Type = "Prefecture" },
                new Models.State() { Name = "Kagawa", Code = "KAGAWA-KEN", Id = 299, CountryId = 89, LocalName = "Kagawa", Type = "Prefecture" },
                new Models.State() { Name = "Kagoshima", Code = "KAGOSHIMA-KEN", Id = 300, CountryId = 89, LocalName = "Kagoshima", Type = "Prefecture" },
                new Models.State() { Name = "Kanagawa", Code = "KANAGAWA-KEN", Id = 301, CountryId = 89, LocalName = "Kanagawa", Type = "Prefecture" },
                new Models.State() { Name = "Kochi", Code = "KOCHI-KEN", Id = 302, CountryId = 89, LocalName = "Kochi", Type = "Prefecture" },
                new Models.State() { Name = "Kumamoto", Code = "KUMAMOTO-KEN", Id = 303, CountryId = 89, LocalName = "Kumamoto", Type = "Prefecture" },
                new Models.State() { Name = "Kyoto", Code = "KYOTO-FU", Id = 304, CountryId = 89, LocalName = "Kyoto", Type = "Prefecture" },
                new Models.State() { Name = "Mie", Code = "MIE-KEN", Id = 305, CountryId = 89, LocalName = "Mie", Type = "Prefecture" },
                new Models.State() { Name = "Miyagi", Code = "MIYAGI-KEN", Id = 306, CountryId = 89, LocalName = "Miyagi", Type = "Prefecture" },
                new Models.State() { Name = "Miyazaki", Code = "MIYAZAKI-KEN", Id = 307, CountryId = 89, LocalName = "Miyazaki", Type = "Prefecture" },
                new Models.State() { Name = "Nagano", Code = "NAGANO-KEN", Id = 308, CountryId = 89, LocalName = "Nagano", Type = "Prefecture" },
                new Models.State() { Name = "Nagasaki", Code = "NAGASAKI-KEN", Id = 309, CountryId = 89, LocalName = "Nagasaki", Type = "Prefecture" },
                new Models.State() { Name = "Nara", Code = "NARA-KEN", Id = 310, CountryId = 89, LocalName = "Nara", Type = "Prefecture" },
                new Models.State() { Name = "Niigata", Code = "NIIGATA-KEN", Id = 311, CountryId = 89, LocalName = "Niigata", Type = "Prefecture" },
                new Models.State() { Name = "Oita", Code = "OITA-KEN", Id = 312, CountryId = 89, LocalName = "Oita", Type = "Prefecture" },
                new Models.State() { Name = "Okayama", Code = "OKAYAMA-KEN", Id = 313, CountryId = 89, LocalName = "Okayama", Type = "Prefecture" },
                new Models.State() { Name = "Okinawa", Code = "OKINAWA-KEN", Id = 314, CountryId = 89, LocalName = "Okinawa", Type = "Prefecture" },
                new Models.State() { Name = "Osaka", Code = "OSAKA-FU", Id = 315, CountryId = 89, LocalName = "Osaka", Type = "Prefecture" },
                new Models.State() { Name = "Saga", Code = "SAGA-KEN", Id = 316, CountryId = 89, LocalName = "Saga", Type = "Prefecture" },
                new Models.State() { Name = "Saitama", Code = "SAITAMA-KEN", Id = 317, CountryId = 89, LocalName = "Saitama", Type = "Prefecture" },
                new Models.State() { Name = "Shiga", Code = "SHIGA-KEN", Id = 318, CountryId = 89, LocalName = "Shiga", Type = "Prefecture" },
                new Models.State() { Name = "Shimane", Code = "SHIMANE-KEN", Id = 319, CountryId = 89, LocalName = "Shimane", Type = "Prefecture" },
                new Models.State() { Name = "Shizuoka", Code = "SHIZUOKA-KEN", Id = 320, CountryId = 89, LocalName = "Shizuoka", Type = "Prefecture" },
                new Models.State() { Name = "Tochigi", Code = "TOCHIGI-KEN", Id = 321, CountryId = 89, LocalName = "Tochigi", Type = "Prefecture" },
                new Models.State() { Name = "Tokushima", Code = "TOKUSHIMA-KEN", Id = 322, CountryId = 89, LocalName = "Tokushima", Type = "Prefecture" },
                new Models.State() { Name = "Tokyo", Code = "TOKYO-TO", Id = 323, CountryId = 89, LocalName = "Tokyo", Type = "Prefecture" },
                new Models.State() { Name = "Tottori", Code = "TOTTORI-KEN", Id = 324, CountryId = 89, LocalName = "Tottori", Type = "Prefecture" },
                new Models.State() { Name = "Toyama", Code = "TOYAMA-KEN", Id = 325, CountryId = 89, LocalName = "Toyama", Type = "Prefecture" },
                new Models.State() { Name = "Wakayama", Code = "WAKAYAMA-KEN", Id = 326, CountryId = 89, LocalName = "Wakayama", Type = "Prefecture" },
                new Models.State() { Name = "Yamagata", Code = "YAMAGATA-KEN", Id = 327, CountryId = 89, LocalName = "Yamagata", Type = "Prefecture" },
                new Models.State() { Name = "Yamaguchi", Code = "YAMAGUCHI-KEN", Id = 328, CountryId = 89, LocalName = "Yamaguchi", Type = "Prefecture" },
                new Models.State() { Name = "Yamanashi", Code = "YAMANASHI-KEN", Id = 329, CountryId = 89, LocalName = "Yamanashi", Type = "Prefecture" },
                new Models.State() { Name = "Aguascalientes", Code = "AGS", Id = 330, CountryId = 114, LocalName = "Aguascalientes", Type = "State" },
                new Models.State() { Name = "Baja California", Code = "BC", Id = 331, CountryId = 114, LocalName = "Baja California", Type = "State" },
                new Models.State() { Name = "Baja California Sur", Code = "BCS", Id = 332, CountryId = 114, LocalName = "Baja California Sur", Type = "State" },
                new Models.State() { Name = "Campeche", Code = "CAMP", Id = 333, CountryId = 114, LocalName = "Campeche", Type = "State" },
                new Models.State() { Name = "Chiapas", Code = "CHIS", Id = 334, CountryId = 114, LocalName = "Chiapas", Type = "State" },
                new Models.State() { Name = "Chihuahua", Code = "CHIH", Id = 335, CountryId = 114, LocalName = "Chihuahua", Type = "State" },
                new Models.State() { Name = "Ciudad de México", Code = "CDMX", Id = 336, CountryId = 114, LocalName = "Ciudad de México", Type = "State" },
                new Models.State() { Name = "Coahuila", Code = "COAH", Id = 337, CountryId = 114, LocalName = "Coahuila", Type = "State" },
                new Models.State() { Name = "Colima", Code = "COL", Id = 338, CountryId = 114, LocalName = "Colima", Type = "State" },
                new Models.State() { Name = "Distrito Federal", Code = "DF", Id = 339, CountryId = 114, LocalName = "Distrito Federal", Type = "State" },
                new Models.State() { Name = "Durango", Code = "DGO", Id = 340, CountryId = 114, LocalName = "Durango", Type = "State" },
                new Models.State() { Name = "Estado de México", Code = "MEX", Id = 341, CountryId = 114, LocalName = "Estado de México", Type = "State" },
                new Models.State() { Name = "Guanajuato", Code = "GTO", Id = 342, CountryId = 114, LocalName = "Guanajuato", Type = "State" },
                new Models.State() { Name = "Guerrero", Code = "GRO", Id = 343, CountryId = 114, LocalName = "Guerrero", Type = "State" },
                new Models.State() { Name = "Hidalgo", Code = "HGO", Id = 344, CountryId = 114, LocalName = "Hidalgo", Type = "State" },
                new Models.State() { Name = "Jalisco", Code = "JAL", Id = 345, CountryId = 114, LocalName = "Jalisco", Type = "State" },
                new Models.State() { Name = "Michoacán", Code = "MICH", Id = 346, CountryId = 114, LocalName = "Michoacán", Type = "State" },
                new Models.State() { Name = "Morelos", Code = "MOR", Id = 347, CountryId = 114, LocalName = "Morelos", Type = "State" },
                new Models.State() { Name = "Nayarit", Code = "NAY", Id = 348, CountryId = 114, LocalName = "Nayarit", Type = "State" },
                new Models.State() { Name = "Nuevo León", Code = "NL", Id = 349, CountryId = 114, LocalName = "Nuevo León", Type = "State" },
                new Models.State() { Name = "Oaxaca", Code = "OAX", Id = 350, CountryId = 114, LocalName = "Oaxaca", Type = "State" },
                new Models.State() { Name = "Puebla", Code = "PUE", Id = 351, CountryId = 114, LocalName = "Puebla", Type = "State" },
                new Models.State() { Name = "Querétaro", Code = "QRO", Id = 352, CountryId = 114, LocalName = "Querétaro", Type = "State" },
                new Models.State() { Name = "Quintana Roo", Code = "Q ROO", Id = 353, CountryId = 114, LocalName = "Quintana Roo", Type = "State" },
                new Models.State() { Name = "San Luis Potosí", Code = "SLP", Id = 354, CountryId = 114, LocalName = "San Luis Potosí", Type = "State" },
                new Models.State() { Name = "Sinaloa", Code = "SIN", Id = 355, CountryId = 114, LocalName = "Sinaloa", Type = "State" },
                new Models.State() { Name = "Sonora", Code = "SON", Id = 356, CountryId = 114, LocalName = "Sonora", Type = "State" },
                new Models.State() { Name = "Tabasco", Code = "TAB", Id = 357, CountryId = 114, LocalName = "Tabasco", Type = "State" },
                new Models.State() { Name = "Tamaulipas", Code = "TAMPS", Id = 358, CountryId = 114, LocalName = "Tamaulipas", Type = "State" },
                new Models.State() { Name = "Tlaxcala", Code = "TLAX", Id = 359, CountryId = 114, LocalName = "Tlaxcala", Type = "State" },
                new Models.State() { Name = "Veracruz", Code = "VER", Id = 360, CountryId = 114, LocalName = "Veracruz", Type = "State" },
                new Models.State() { Name = "Yucatán", Code = "YUC", Id = 361, CountryId = 114, LocalName = "Yucatán", Type = "State" },
                new Models.State() { Name = "Zacatecas", Code = "ZAC", Id = 362, CountryId = 114, LocalName = "Zacatecas", Type = "State" },
                new Models.State() { Name = "Alabama", Code = "AL", Id = 363, CountryId = 192, LocalName = "Alabama", Type = "State" },
                new Models.State() { Name = "Alaska", Code = "AK", Id = 364, CountryId = 192, LocalName = "Alaska", Type = "State" },
                new Models.State() { Name = "Arizona", Code = "AZ", Id = 365, CountryId = 192, LocalName = "Arizona", Type = "State" },
                new Models.State() { Name = "Arkansas", Code = "AR", Id = 366, CountryId = 192, LocalName = "Arkansas", Type = "State" },
                new Models.State() { Name = "California", Code = "CA", Id = 367, CountryId = 192, LocalName = "California", Type = "State" },
                new Models.State() { Name = "Colorado", Code = "CO", Id = 368, CountryId = 192, LocalName = "Colorado", Type = "State" },
                new Models.State() { Name = "Connecticut", Code = "CT", Id = 369, CountryId = 192, LocalName = "Connecticut", Type = "State" },
                new Models.State() { Name = "Delaware", Code = "DE", Id = 370, CountryId = 192, LocalName = "Delaware", Type = "State" },
                new Models.State() { Name = "District of Columbia", Code = "DC", Id = 371, CountryId = 192, LocalName = "District of Columbia", Type = "State" },
                new Models.State() { Name = "Florida", Code = "FL", Id = 372, CountryId = 192, LocalName = "Florida", Type = "State" },
                new Models.State() { Name = "Georgia", Code = "GA", Id = 373, CountryId = 192, LocalName = "Georgia", Type = "State" },
                new Models.State() { Name = "Hawaii", Code = "HI", Id = 374, CountryId = 192, LocalName = "Hawaii", Type = "State" },
                new Models.State() { Name = "Idaho", Code = "ID", Id = 375, CountryId = 192, LocalName = "Idaho", Type = "State" },
                new Models.State() { Name = "Illinois", Code = "IL", Id = 376, CountryId = 192, LocalName = "Illinois", Type = "State" },
                new Models.State() { Name = "Indiana", Code = "IN", Id = 377, CountryId = 192, LocalName = "Indiana", Type = "State" },
                new Models.State() { Name = "Iowa", Code = "IA", Id = 378, CountryId = 192, LocalName = "Iowa", Type = "State" },
                new Models.State() { Name = "Kansas", Code = "KS", Id = 379, CountryId = 192, LocalName = "Kansas", Type = "State" },
                new Models.State() { Name = "Kentucky", Code = "KY", Id = 380, CountryId = 192, LocalName = "Kentucky", Type = "State" },
                new Models.State() { Name = "Louisiana", Code = "LA", Id = 381, CountryId = 192, LocalName = "Louisiana", Type = "State" },
                new Models.State() { Name = "Maine", Code = "ME", Id = 382, CountryId = 192, LocalName = "Maine", Type = "State" },
                new Models.State() { Name = "Maryland", Code = "MD", Id = 383, CountryId = 192, LocalName = "Maryland", Type = "State" },
                new Models.State() { Name = "Massachusetts", Code = "MA", Id = 384, CountryId = 192, LocalName = "Massachusetts", Type = "State" },
                new Models.State() { Name = "Michigan", Code = "MI", Id = 385, CountryId = 192, LocalName = "Michigan", Type = "State" },
                new Models.State() { Name = "Minnesota", Code = "MN", Id = 386, CountryId = 192, LocalName = "Minnesota", Type = "State" },
                new Models.State() { Name = "Mississippi", Code = "MS", Id = 387, CountryId = 192, LocalName = "Mississippi", Type = "State" },
                new Models.State() { Name = "Missouri", Code = "MO", Id = 388, CountryId = 192, LocalName = "Missouri", Type = "State" },
                new Models.State() { Name = "Montana", Code = "MT", Id = 389, CountryId = 192, LocalName = "Montana", Type = "State" },
                new Models.State() { Name = "Nebraska", Code = "NE", Id = 390, CountryId = 192, LocalName = "Nebraska", Type = "State" },
                new Models.State() { Name = "Nevada", Code = "NV", Id = 391, CountryId = 192, LocalName = "Nevada", Type = "State" },
                new Models.State() { Name = "New Hampshire", Code = "NH", Id = 392, CountryId = 192, LocalName = "New Hampshire", Type = "State" },
                new Models.State() { Name = "New Jersey", Code = "NJ", Id = 393, CountryId = 192, LocalName = "New Jersey", Type = "State" },
                new Models.State() { Name = "New Mexico", Code = "NM", Id = 394, CountryId = 192, LocalName = "New Mexico", Type = "State" },
                new Models.State() { Name = "New York", Code = "NY", Id = 395, CountryId = 192, LocalName = "New York", Type = "State" },
                new Models.State() { Name = "North Carolina", Code = "NC", Id = 396, CountryId = 192, LocalName = "North Carolina", Type = "State" },
                new Models.State() { Name = "North Dakota", Code = "ND", Id = 397, CountryId = 192, LocalName = "North Dakota", Type = "State" },
                new Models.State() { Name = "Ohio", Code = "OH", Id = 398, CountryId = 192, LocalName = "Ohio", Type = "State" },
                new Models.State() { Name = "Oklahoma", Code = "OK", Id = 399, CountryId = 192, LocalName = "Oklahoma", Type = "State" },
                new Models.State() { Name = "Oregon", Code = "OR", Id = 400, CountryId = 192, LocalName = "Oregon", Type = "State" },
                new Models.State() { Name = "Pennsylvania", Code = "PA", Id = 401, CountryId = 192, LocalName = "Pennsylvania", Type = "State" },
                new Models.State() { Name = "Puerto Rico", Code = "PR", Id = 402, CountryId = 192, LocalName = "Puerto Rico", Type = "State" },
                new Models.State() { Name = "Rhode Island", Code = "RI", Id = 403, CountryId = 192, LocalName = "Rhode Island", Type = "State" },
                new Models.State() { Name = "South Carolina", Code = "SC", Id = 404, CountryId = 192, LocalName = "South Carolina", Type = "State" },
                new Models.State() { Name = "South Dakota", Code = "SD", Id = 405, CountryId = 192, LocalName = "South Dakota", Type = "State" },
                new Models.State() { Name = "Tennessee", Code = "TN", Id = 406, CountryId = 192, LocalName = "Tennessee", Type = "State" },
                new Models.State() { Name = "Texas", Code = "TX", Id = 407, CountryId = 192, LocalName = "Texas", Type = "State" },
                new Models.State() { Name = "Utah", Code = "UT", Id = 408, CountryId = 192, LocalName = "Utah", Type = "State" },
                new Models.State() { Name = "Vermont", Code = "VT", Id = 409, CountryId = 192, LocalName = "Vermont", Type = "State" },
                new Models.State() { Name = "Virginia", Code = "VA", Id = 410, CountryId = 192, LocalName = "Virginia", Type = "State" },
                new Models.State() { Name = "Washington", Code = "WA", Id = 411, CountryId = 192, LocalName = "Washington", Type = "State" },
                new Models.State() { Name = "West Virginia", Code = "WV", Id = 412, CountryId = 192, LocalName = "West Virginia", Type = "State" },
                new Models.State() { Name = "Wisconsin", Code = "WI", Id = 413, CountryId = 192, LocalName = "Wisconsin", Type = "State" },
                new Models.State() { Name = "Wyoming", Code = "WY", Id = 414, CountryId = 192, LocalName = "Wyoming", Type = "State" }                
                );
            #endregion


            #endregion;

            #endregion

            base.OnModelCreating(modelBuilder);


        }

        #endregion
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SiteImage> SiteImage { get; set; }
        public DbSet<Cookbook> Cookbook { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Instruction> Instruction { get; set; }
        public DbSet<PostalCode> PostalCode { get; set; }
        public DbSet<Geocode> Geocode { get; set; }
        public DbSet<WebReference> WebReference { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Profile> Profile { get; set; }

        #region location tables
        public DbSet<Community> Community { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }
        

        #endregion        

        #region Relationship Tables
        public DbSet<CommunityCookbook> CommunityCookbook { get; set; }
        public DbSet<CookbookCategory> CookbookCategory { get; set; }
        public DbSet<CommunityState> CommunityState { get; set; }
        public DbSet<OrganizationCookbook> OrganizationCookbook { get; set; }
        public DbSet<OrganizationCommunity> OrganizationCommunity { get; set; }

        public DbSet<CookbookRecipe> CookbookRecipes { get; set; }
        
        public DbSet<RecipeUserImage> RecipeUserImage { get; set; }
        public DbSet<RecipeCategory> RecipeCategory { get; set; }        
        public DbSet<CommunityRecipe> CommunityRecipe { get; set; }

        public DbSet<CommunityGeocode> CommunityGeocode { get; set; }
        public DbSet<CommunityPostalCode> CommunityPostalCode { get; set; }
        public DbSet<CommunityWebReference> CommunityWebReference { get; set; }

        public DbSet<PostalCodeGeocode> PostalCodeGeocode { get; set; }

        public DbSet<ApplicationUserRecipe> ApplicationUserRecipe { get; set; }
        public DbSet<ApplicationUserCookbook> ApplicationUserCookbook { get; set; }
        public DbSet<ApplicationUserUserImage> ApplicationUserUserImage { get; set; }
        public DbSet<ApplicationUserFeed> ApplicationUserFeed { get; set; }
        public DbSet<ApplicationUserProfile> ApplicationUserProfile { get; set; }

        public DbSet<FeedCategory> FeedCategory { get; set; }
        public DbSet<FeedCommunity> FeedCommunity { get; set; }
        public DbSet<FeedCookbook> FeedCookbook { get; set; }
        public DbSet<FeedCountry> FeedCountry { get; set; }
        public DbSet<FeedOrganization> FeedOrganization { get; set; }
        public DbSet<FeedProfile> FeedProfile { get; set; }
        public DbSet<FeedRecipe> FeedRecipe { get; set; }
        public DbSet<FeedState> FeedState { get; set; }
        public DbSet<FeedUserImage> FeedUserImage { get; set; }
        public DbSet<FeedTopic> FeedTopic { get; set; }
        // NOTE: Start making these names NOT PLURAL

        #endregion

    }
}
