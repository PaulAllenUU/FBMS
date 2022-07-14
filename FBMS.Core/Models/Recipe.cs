using System;
namespace FBMS.Core.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PreparationTime { get; set; } 

        public string CookingTime { get; set; }

        public bool Vegetarian { get; set; }

        public bool CoeliacFriendly { get; set; }

        public string MeatType { get; set; }

        public IList<RecipeIngredients> Recipes { get; set; } = new List<RecipeIngredients>();
    }
}