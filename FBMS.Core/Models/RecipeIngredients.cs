using System;
namespace FBMS.Core.Models
{
    public class RecipeIngredients
    {
        public int Id { get; set; }

        public int IngredientQuantity { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        //the recipeingredients class will always generate a list of recipes
        //even when the list is empty the list will still exist

    }
}