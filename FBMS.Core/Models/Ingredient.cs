using System;
namespace FBMS.Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int RecipeIngredientId { get; set; }

        public IList<RecipeIngredients> RecipeIngredients { get; set; } = new List<RecipeIngredients>();

    }
}