using System;
namespace FBMS.Core.Models
{

    //bridging class which normalises the relationship between parcels and recipes
    public class ParcelRecipe
    {
        public int ParcelId { get; set; }

        public int RecipeId { get; set; }
    }
}