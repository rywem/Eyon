using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.XTests.UnitTests
{
    public class SampleData
    {

        public static void SeedDatabase( IUnitOfWork unitOfWork, List<Country> countries, List<State> states, List<Community> communities,
           List<ApplicationUser> applicationUsers, List<Cookbook> cookbooks, List<Category> categories, List<Organization> organizations )
        {
            Country country = new Country()
            {
                Code = "US",
                Name = "UNITED STATES"
            };
            unitOfWork.Country.Add(country);
            unitOfWork.Save();
            countries.Add(country);

            State state = new State()
            {
                Name = "California",
                LocalName = "California",
                Code = "CA",
                Type = "State",
                CountryId = country.Id
            };
            unitOfWork.State.Add(state);
            unitOfWork.Save();
            states.Add(state);

            Community community = new Community() { Name = "QUINCY", Active = true, CountryId = country.Id };
            unitOfWork.Community.Add(community);
            unitOfWork.Save();
            communities.Add(community);
            unitOfWork.CommunityState.AddFromEntities(community, state);
            Community community2 = new Community() { Name = "SANTA MARIA", Active = true, CountryId = country.Id };
            unitOfWork.Community.Add(community2);
            communities.Add(community2);
            unitOfWork.CommunityState.AddFromEntities(community2, state);

            ApplicationUser user1 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Ryan",
                LastName = "Wemmer"
            };
            ApplicationUser user2 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Kelly",
                LastName = "Wemmer"
            };
            unitOfWork.ApplicationUser.Add(user1);
            unitOfWork.ApplicationUser.Add(user2);
            unitOfWork.Save();

            applicationUsers.Add(user1);
            applicationUsers.Add(user2);
            // add cookbooks
            // add categories
            Cookbook cookbook1 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2020",
                Name = "Breakfast Time",
                Description = "Ryan's Breakfast Favorites",
                Privacy = Models.Enums.Privacy.Public
            };
            unitOfWork.Cookbook.Add(cookbook1);


            Cookbook cookbook2 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2018",
                Name = "Dinner Explorer",
                Description = "Exploring the greatness that can be dinner.",
                Privacy = Models.Enums.Privacy.Private
            };
            unitOfWork.Cookbook.Add(cookbook2);
            Cookbook cookbook3 = new Cookbook()
            {
                Author = "Kelly Wemmer",
                Copyright = "2019",
                Name = "Sweets and Treats",
                Description = "My favorite desserts and sweets.",
                Privacy = Models.Enums.Privacy.Public
            };
            unitOfWork.Cookbook.Add(cookbook3);
            Cookbook cookbook4 = new Cookbook()
            {
                Author = "Ryan Wemmer",
                Copyright = "2017",
                Name = "Just Cookies",
                Description = "Ryan's Best Cookies",
                Privacy = Models.Enums.Privacy.Public
            };

            unitOfWork.Cookbook.Add(cookbook4);
            unitOfWork.Save();
            cookbooks.Add(cookbook1);
            cookbooks.Add(cookbook2);
            cookbooks.Add(cookbook3);
            cookbooks.Add(cookbook4);

            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook1);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook2);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user2, cookbook3);
            unitOfWork.ApplicationUserCookbook.AddFromEntities(user1, cookbook4);
            unitOfWork.Save();

            Category category1 = new Category()
            {
                Name = "Dinner",
                DisplayOrder = 0
            };
            unitOfWork.Category.Add(category1);

            Category category2 = new Category()
            {
                Name = "Beef",
                DisplayOrder = 1
            };

            unitOfWork.Category.Add(category2);
            Category category3 = new Category()
            {
                Name = "Quick and Easy",
                DisplayOrder = 2
            };
            unitOfWork.Category.Add(category3);
            unitOfWork.Save();
            categories.Add(category1);
            categories.Add(category2);
            categories.Add(category3);

            Organization organization = new Organization()
            {
                Name = "Cooks and Co",
                Privacy = Models.Enums.Privacy.Public,
                Description = "Organization for cooks.",
                Type = "Group"
            };
            unitOfWork.Organization.Add(organization);
            unitOfWork.Save();

            organizations.Add(organization);
        }
    }
}
