using Eyon.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Eyon.XTests.UnitTests.Cases
{
    public class RecipeViewModelData : IEnumerable<object[]>
    {
        private IEnumerable<object[]> data => new[]
        {
            new object[]
            {
                new RecipeViewModel
                {
                    Recipe = new Models.Recipe()
                    {
                        Name = "Hotcakes",
                        Description = "They're sellin' like hotcakes!",
                        PrepTime = "10 mins",
                        Servings = "2-4",
                        Cooktime = "10 mins",
                        Privacy = Models.Enums.Privacy.Private
                    },
                    IngredientText = @"1 cup all-purpose flour
1 cup milk
4 oz butter
3 tbspn sugar
1 egg
1 tsp baking powder",
                    InstructionText = @"Melt butter in large bowl.
Add egg, sugar, milk and stir gently until egg is beaten.
Add flour, mix until smooth.
Add baking powder and stir.
Heat griddle to medium heat.
Add very small amount of butter to griddle (start with a few drops!)
Add tablespoons of batter to griddle. Let rise before flipping."
                }
            }, 
            new object[]
            {
                new RecipeViewModel
                {
                    Recipe = new Models.Recipe()
                    {
                        Name = "Hobo Stew",
                        Description = "A quick and easy dinner.",
                        PrepTime = "10 mins",
                        Servings = "4",
                        Cooktime = "15 mins",
                        Privacy = Models.Enums.Privacy.Public
                    },
                    IngredientText = @"1 lb ground beef
1 medium onion
3 cloves garlic
1 can tomato sauce
1 can pinto beans
1 can corn
1 can green beans
1 can peas and carrots
2-3 beef bouillon cubes
1/2 tsp pepper
1 cup cooked rice (optional)",
                    InstructionText = @"In a medium pot, saute onions until soft.
Add beef, cook until brown.
Add garlic, until fragrant.
Drain excess juices.
Add all remaining ingredients, heat until warm.
Serve and enjoy!"
                }
            },
            new object[]
            {
                new RecipeViewModel
                {
                    Recipe = new Models.Recipe()
                    {
                        Name = "Ham and Rice Dinner",
                        Description = "Ham and rice for a wonderful dinner.",
                        PrepTime = "15 mins",
                        Servings = "Serves 6",
                        Cooktime = "10 mins",
                        Privacy = Models.Enums.Privacy.Public
                    },
                    IngredientText = @"3 cups hot cooked rice
2 1/2 cups cooked ham, cut into strips
1-2 tbsp butter
1/2 cup chopped onions
2 cups chopped celery
1 can cream of chick soup
2 tbsp white wine (optional)
1 1/2 tspn yellow mustard
1/4 tspn dill
3/4 cup sour cream 
1/3 cup sliced pimiento",
                    InstructionText = @"While rice is cooking, saute ham in butter for 2 minutes. 
Add onions and celery to ham, saute on medium heat until tender.
Stir in soup, mustard, dill weed and optional white wine. Heat thoroughly.
Add sour cream and pimiento. Heat but do not boil. 
Serve of hot rice."
                }
            }
        };
        public IEnumerator<object[]> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}
