
using FBMS.Core.Models;
using FBMS.Core.Services;
using FBMS.Core.Security;
using FBMS.Data.Repositories;
namespace FBMS.Data.Services
{
    public class UserServiceDb : IUserService
    {
        private readonly DatabaseContext  ctx;

        public UserServiceDb()
        {
            ctx = new DatabaseContext(); 
        }

        public void Initialise()
        {
           ctx.Initialise(); 
        }

        // ------------------ User Related Operations ------------------------

        // retrieve list of Users
        public IList<User> GetUsers()
        {
            return ctx.Users.ToList();
        }

        // Retrive User by Id 
        public User GetUser(int id)
        {
            return ctx.Users.FirstOrDefault(s => s.Id == id);
        }

        // Add a new User checking a User with same email does not exist
        public User AddUser(string firstName, string surName, string postCode, string email, string password, Role role)
        {     
            var existing = GetUserByEmail(email);
            if (existing != null)
            {
                return null;
            } 

            var user = new User
            {            
                FirstName = firstName,
                SurName = surName,
                PostCode = postCode, 
                Email = email,
                Password = Hasher.CalculateHash(password), // can hash if required 
                Role = role              
            };
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user; // return newly added User
        }

        // Delete the User identified by Id returning true if deleted and false if not found
        public bool DeleteUser(int id)
        {
            var s = GetUser(id);
            if (s == null)
            {
                return false;
            }
            ctx.Users.Remove(s);
            ctx.SaveChanges();
            return true;
        }

        // Update the User with the details in updated 
        public User UpdateUser(User updated)
        {
            // verify the User exists
            var User = GetUser(updated.Id);
            if (User == null)
            {
                return null;
            }
            // verify email address is registered or available to this user
            if (!IsEmailAvailable(updated.Email, updated.Id))
            {
                return null;
            }
            // update the details of the User retrieved and save
            User.FirstName = updated.FirstName;
            User.SurName = updated.SurName;
            User.PostCode = updated.PostCode;
            User.Email = updated.Email;
            User.Password = Hasher.CalculateHash(updated.Password);  
            User.Role = updated.Role; 

            ctx.SaveChanges();          
            return User;
        }

        // Find a user with specified email address
        public User GetUserByEmail(string email)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        // Verify if email is available or registered to specified user
        public bool IsEmailAvailable(string email, int userId)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email && u.Id != userId) == null;
        }

        public IList<User> GetUsersQuery(Func<User,bool> q)
        {
            return ctx.Users.Where(q).ToList();
        }

        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
            //return (user != null && user.Password == password ) ? user: null;
        }

        IList<Stock> IUserService.GetAllStock()
        {
            return ctx.Stock.ToList();
        }

        IList<Stock> IUserService.GetStockByExpiryDate()
        {
            throw new NotImplementedException();
        }

        IList<Stock> IUserService.GetStockByDescription()
        {
            throw new NotImplementedException();
        }

        IList<Stock> IUserService.GetNonFoodItems()
        {
            throw new NotImplementedException();
        }

        IList<Stock> IUserService.GetAllFoodItems()
        {
            throw new NotImplementedException();
        }

        Stock IUserService.GetStockById(int id)
        {
            throw new NotImplementedException();
        }

        Stock IUserService.AddStock(string description, string colour, bool foodItem, DateTime expiryDate)
        {
            throw new NotImplementedException();
        }

        Stock IUserService.UpdateStock(Stock stock)
        {
            throw new NotImplementedException();
        }

        bool IUserService.DeleteStock(int id)
        {
            throw new NotImplementedException();
        }

        IList<Ingredient> IUserService.GetAllIngredients()
        {
            throw new NotImplementedException();
        }

        IList<Ingredient> IUserService.GetIngredientByDescription()
        {
            throw new NotImplementedException();
        }

        Ingredient IUserService.AddIngredient(string description, int StockId)
        {
            throw new NotImplementedException();
        }

        Ingredient IUserService.GetIngredientById(int id)
        {
            throw new NotImplementedException();
        }

        Ingredient IUserService.UpdateIngredient(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        bool IUserService.DeleteIngredient(int id, string description)
        {
            throw new NotImplementedException();
        }

        bool IUserService.IsIngredientAvailableFromStock(int id, int stockId, string description)
        {
            throw new NotImplementedException();
        }
    }
}