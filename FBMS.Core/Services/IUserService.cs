
using FBMS.Core.Models;

namespace FBMS.Core.Services
{
    // This interface describes the operations that a UserService class implementation should provide
    public interface IUserService
    {
        // Initialise the repository - only to be used during development 
        void Initialise();

        // ---------------- Begin User Management --------------
        IList<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        bool IsEmailAvailable(string email, int userId);
        User AddUser(string firstName, string surName, string postCode, string email, string password, Role role);
        User UpdateUser(User user);
        bool DeleteUser(int id);
        
        IList<User> GetUsersQuery(Func<User,bool> q);
        User Authenticate(string email, string password);

        //-----------------End of user management----------

        //---------------Begin Ticket Management------------
        IList<Ticket> GetAllTickets();

        IList<Ticket> GetTicketsWithDietaryRequirements(int id, int dietaryRequirementId);

        IList<Ticket> GetTicketsCreatedOnCertainDay(DateTime createdOn);

        IList<Ticket> GetTicketsForWhenParcelRequired(DateTime parcelRequired);

        IList<Ticket> GetTicketsWithDietaryRequirementNeededOnSpecificDay(int dietaryRequirementId, DateTime parcelRequired);

        Ticket GetTicketById(int id);

        Ticket AddTicket(int noOfPeople, DateTime dateRequired, int userId, int dietaryRequirementId);

        Ticket UpdateTicket(Ticket updated);

        bool DeleteTicket(int id);

        //---------------End of Ticket Management----------


        // --------------Begin Stock Management --------
        IList<Stock> GetAllStock();

        IList<Stock> SortStockByExpiryDate();

        IList<Stock> GetAllStockWithSpecificExpiry(DateOnly expiryDate);

        IList<Stock> SortByExpiryDateReverse();

        IList<Stock> GetStockByDescription(string description);

        IList<Stock> GetNonFoodItems();

        IList<Stock> GetAllFoodItems();

        Stock GetStockById(int id);
        
        Stock AddStock(int id, string description, string colour, bool foodItem, DateOnly expiryDate);

        Stock UpdateStock (Stock stock);

        bool DeleteStock(int id);

        //-------------------End of Stock Management---------

        //----------------- Begin Ingredient Management--------
        IList<Ingredient> GetAllIngredients();

        IList<Ingredient> GetIngredientsByDescription(string description);

        Ingredient AddIngredient(int id, int stockId, string description);

        Ingredient GetIngredient(int id, int stockId);

        Ingredient UpdateIngredient(Ingredient ingredient);

        bool DeleteIngredient(int id, int stockId, string description);

        bool IsIngredientAvailableFromStock(int id, int stockId, string description);
        
        //-------------End of Ingredient Management------------

        //-------Begin DropOffPoint Management------------
        IList<StockDrop> GetAllStockDrops();

        StockDrop GetStockDropByPostCodeAndDate(string postCode, DateTime date);

        IList<StockDrop> GetStockDropsForSpecificDate(DateTime date);

        StockDrop AddStockDrop(int streetNumber, string streetName, DateTime date, string postCode, int parcelId);

        StockDrop GetStockDropById(int id);

        StockDrop UpdateStockDrop(StockDrop stockDrop);

        bool DeleteStockDrop (int id);


        //-------------Begin RecipeIngredientManagement (Intermediate Class)------

        IList<RecipeIngredients> GetAllRecipeIngredients();

        IList<RecipeIngredients> GetRecipeIngredientsWithSpecificQty(int quantity);

        RecipeIngredients GetRecipeIngredientById(int id);

        RecipeIngredients AddIngredientToRecipeIngredients(int ingredientId, int recipeId, int ingredientQuantity);

        bool RemoveIngredientFromRecipeIngredients(int ingredientId, int recipeId);

        RecipeIngredients UpdateRecipeIngredientsQuantity(int recipeId, int ingredientId, int ingredientQuantity);

        //---------End of RecipeIngredient Management----------


        //-------------Begin Recipe Management------------
        IList<Recipe> GetAllRecipes();

        IList<Recipe> GetVegetarianRecipes(bool vegetarian);

        IList<Recipe> GetCoeliacFriendlyRecipes(bool coeliacFriendly);

        IList<Recipe> GetRecipesSpecifiedByMeatType(string meatType);

        IList<Recipe> GetRecipesUnderSpecificTime(int cookingTimeInMins);

        IList<Recipe> GetVegetarianRecipesUnderASpecificTime(bool vegetarian, int cookingTimeMins);

        IList<Recipe> GetCoeliacRecipesUnderASpecificTime(bool coeliacFriendly, int cookingTime);

        IList<Recipe> GetCertainMeatUnderSpecificTime(string meatType, int cookingTime);

        Recipe GetRecipeById(int id);

        Recipe GetRecipeByName(string name);

        Recipe AddRecipe(string name, bool vegetarian, bool coeliacFriendly, int cookingTimeMins, string meatType);

        Recipe UpDateRecipe(Recipe updated);

        bool DeleteRecipe(int id, string Name);

        //----------------End of Recipe Management-----------

    























        
       
    }
    
}
