using System;
using Eyon.XTests.UnitTests.DataAccess.Data.Orchestator;
namespace Eyon.XConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CookbookOrchestratorTests cookbookOrchestratorTests = new CookbookOrchestratorTests();

            cookbookOrchestratorTests.CookbookViewModel_AddCategoryRelationshipToExistingCookbook_CookbookCategoriesCountEqual1();
        }
    }
}
