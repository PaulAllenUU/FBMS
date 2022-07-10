using System;
namespace FBMS.Core.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PreparatioTime { get; set; } 

        public string CookingTime { get; set; }

        public bool Vegetarian { get; set; }

        public bool CoeliacFriendly { get; set; }

        public string MeatType { get; set; }

        IList<RecipeIngredients> RecipeIngredients { get; set; } = new List<RecipeIngredients>();
    }
}