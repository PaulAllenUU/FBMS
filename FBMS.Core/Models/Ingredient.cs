using System;
namespace FBMS.Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public IList<RecipeIngredients> Recipes { get; set; } = new List<RecipeIngredients>();

    }
}