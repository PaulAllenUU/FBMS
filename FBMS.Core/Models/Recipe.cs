using System;
namespace FBMS.Core.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Vegetarian { get; set; }

        public bool CoeliacFriendly { get; set; }

        public int CookingTimeMins { get; set; }

        public string MeatType { get; set; }

        public IList<RecipeIngredients> RecipeIngredients { get; set; } = new List<RecipeIngredients>();
    }
}