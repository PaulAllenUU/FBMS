
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

        // ---------------- Begin Stock Management --------------
        IList<Stock> GetAllStock();

        IList<Stock> GetStockByExpiryDate();

        IList<Stock> GetStockByDescription();

        IList<Stock> GetNonFoodItems();

        IList<Stock> GetAllFoodItems();

        Stock GetStockById(int id);
        
        Stock AddStock(string description, string colour, bool foodItem, DateTime expiryDate);

        Stock UpdateStock (Stock stock);

        bool DeleteStock(int id);

        //-------------------End of Stock Management---------

        //----------------- Begin Ingredient Management--------
        IList<Ingredient> GetAllIngredients();

        IList<Ingredient> GetIngredientByDescription();

        Ingredient AddIngredient(string description, int StockId);

        Ingredient GetIngredientById(int id);

        Ingredient UpdateIngredient(Ingredient ingredient);

        bool DeleteIngredient(int id, string description);

        bool IsIngredientAvailableFromStock(int id, int stockId, string description);












        
       
    }
    
}
