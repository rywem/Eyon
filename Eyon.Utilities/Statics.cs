
namespace Eyon.Utilities
{
    public static class Statics
    {
        public static class Roles
        {
            public const string Admin = "Admin"; // full control of website, can modify any user
            public const string Manager = "Manager"; // Approve communities, add/edit categories
            public const string Seller = "Seller"; // Can add cookbooks, submit commuities for approval, add categories to cookbooks
            public const string Customer = "Customer"; // Can browse cookbooks, submit orders
        }
    }
}
