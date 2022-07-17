
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

            //if existing is not null then do nothing because the user already exists
            if (existing != null)
            {
                return null;
            } 

            //create the new user and assign the attributes
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

            //if the user null, cannot delete a user that does not exist so return false
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

        //search allows to return criteria specified by the user search
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

        //----------End of user management implementation---------

        public IList<Stock> GetAllStock()
        {
            return ctx.Stock.ToList();
        }

        public IList<Stock> SortStockByExpiryDate()
        {
            return ctx.Stock.OrderBy(s => s.ExpiryDate).ToList();
        }

        public IList<Stock> GetAllStockWithSpecificExpiry(DateOnly expiryDate)
        {
            return ctx.Stock.Where(s => s.ExpiryDate == expiryDate).ToList();
        }

        public IList<Stock> SortByExpiryDateReverse()
        {
            return ctx.Stock.OrderByDescending(s => s.ExpiryDate).ToList();
        }

        public IList<Stock> GetStockByDescription(string description)
        {
            return ctx.Stock.Where( s => s.Description == description).ToList();
        }

        public IList<Stock> GetNonFoodItems()
        {
            return ctx.Stock.Where( s => s.FoodItem == false).ToList();
        }

        public IList<Stock> GetAllFoodItems()
        {
            return ctx.Stock.Where ( s => s.FoodItem == true).ToList();
        }

        public Stock GetStockById(int id)
        {
            return ctx.Stock.FirstOrDefault( s => s.Id == id);
        }

        public Stock AddStock(int id, string description, string colour, bool foodItem, DateOnly expiryDate)
        {
           var existing = GetStockById(id);
           
           //if this is not null then do nothing because this stock already exists
           if (existing != null)
           {
                return null;
           }
           
           //if existing is null then add attributes to the new stock item
           var stock = new Stock
           {
                Id = id,
                Description = description,
                Colour = colour,
                FoodItem = foodItem,
                ExpiryDate = expiryDate
           };

           //save changes and return the stock just added
           ctx.Stock.Add(stock);
           ctx.SaveChanges();
           return stock;

        }

        public Stock UpdateStock(Stock updated)
        {
            //to update stock it is necessary to check if the stock exists
            //if it does not exist, ie null then do nothing ie return null
            var stock = GetStockById(updated.Id);

            if (stock == null)
            {
                return null;
            }

            stock.Id = updated.Id;
            stock.Description = updated.Description;
            stock.Colour = updated.Colour;
            stock.FoodItem = updated.FoodItem;
            stock.ExpiryDate = updated.ExpiryDate;

            return stock;

        }

        public bool DeleteStock(int id)
        {
            var s = GetStockById(id);
            if (s == null)
            {
                return false;
            }

            ctx.Stock.Remove(s);
            ctx.SaveChanges();
            return true;
        }

        public IList<Ingredient> GetAllIngredients()
        {
            return ctx.Ingredients.ToList();
        }

        public IList<Ingredient> GetIngredientsByDescription(string description)
        {
            return ctx.Ingredients.Where( s => s.Description == description)
                                  .ToList();
        }

        public Ingredient AddIngredient(int id, int stockId, string description)
        {
            var existing = GetIngredient(id, stockId);
            if (existing != null)
            {
                return null;
            }

            var ingredient = new Ingredient
            {
                Id = id,
                StockId = stockId,
                Description = description,

            };

            ctx.Ingredients.Add(ingredient);
            ctx.SaveChanges();
            return ingredient;
        }

        public Ingredient GetIngredient(int id, int stockId)
        {
            return ctx.Ingredients.FirstOrDefault(s => s.Id == id && s.StockId == stockId);
        }

        public Ingredient UpdateIngredient(Ingredient updated)
        {
            var i = GetIngredient(updated.Id, updated.StockId);
            if (i == null)
            {
                return null;
            }

            i.Id = updated.Id;
            i.StockId = updated.StockId;
            i.Description = updated.Description;

            ctx.SaveChanges();
            return i;
        }

        public bool DeleteIngredient(int id, int stockId, string description)
        {
            var i = GetIngredient(id, stockId);
            if(i == null)
            {
                return false;
            }

            ctx.Ingredients.Remove(i);
            ctx.SaveChanges();
            return true;

        }

        public bool IsIngredientAvailableFromStock(int id, int stockId, string description)
        {
            var i = GetIngredient(id, stockId);

            if (ctx.Stock.Any(s =>s.Id == i.StockId))
            {
                return true;
            }

            return false; 
        }

        public IList<Ticket> GetAllTickets()
        {
            return ctx.Tickets.ToList();
        }

        public IList<Ticket> GetTicketsWithDietaryRequirements(int id, int dietaryRequirementId)
        {
            return ctx.Tickets.Where(t => t.DietaryRequirements != null).ToList();
        }

        public IList<Ticket> GetTicketsCreatedOnCertainDay(DateTime createdOn)
        {
            return ctx.Tickets.Where(t => t.CreatedOn == createdOn).ToList();
            
        }

        public IList<Ticket> GetTicketsForWhenParcelRequired(DateTime parcelRequired)
        {
            return ctx.Tickets.Where(t => t.DateRequired == parcelRequired).ToList();
        }

        public IList<Ticket> GetTicketsWithDietaryRequirementNeededOnSpecificDay(int dietaryRequirementId, DateTime parcelRequired)
        {
            return ctx.Tickets.Where(t => t.DietaryRequirementId == dietaryRequirementId && t.DateRequired == parcelRequired)
                              .ToList();
        }


        public Ticket GetTicketById(int id)
        {
            return ctx.Tickets.FirstOrDefault( t => t.Id == id);
        }

        public Ticket AddTicket(int noOfPeople, DateTime dateRequired, int userId, int dietaryRequirementId)
        {
            //check that the user creating the ticket exists
            var checkuser = GetUser(userId);

            //if the user trying to create the ticket does not exist return null
            if (checkuser == null) return null;

            //otherwise create the new ticket and add all properties
            var ticket = new Ticket
            {
                NoOfPeople = noOfPeople,
                DateRequired = dateRequired,
                UserId = userId,
                DietaryRequirementId = dietaryRequirementId,
                CreatedOn = DateTime.Now,
            };

            ctx.Tickets.Add(ticket);
            ctx.SaveChanges();
            return ticket;

        }

        public Ticket UpdateTicket(Ticket updated)
        {

            //verify the user updating the ticket exists
            var user = GetUser(updated.UserId);

            //if they dont exist then return null
            if (user == null) return null;

            //verify that the ticket exists before it can be updated
            var t = GetTicketById(updated.Id);

            //if the ticket does not exist then return null
            if (t == null) return null;

            //update the details of the food ticket
            t.NoOfPeople = updated.NoOfPeople;
            t.DateRequired = updated.DateRequired;
            t.UserId = updated.UserId;
            t.DietaryRequirementId = updated.DietaryRequirementId;

            ctx.SaveChanges();
            return t;

        }

        public bool DeleteTicket(int id)
        {
            //check that the ticket exists before it can be deleted
            var ticket = GetTicketById(id);

            if (ticket == null) return false;

            var delete = ctx.Tickets.Remove(ticket);

            ctx.SaveChanges();
            return true;
        }

        //--------end of Ticket management

        //-----Begin Stock Dtop Management---------------

        public IList<StockDrop> GetAllStockDrops()
        {
            return ctx.StockDrops.ToList();
        }


        public IList<StockDrop> GetStockDropsByAddress(int streetNumber, string postCode)
        {
            return ctx.StockDrops.Where(x => x.StreetNumber == streetNumber && x.PostCode == postCode)
                                 .ToList();
        }

        public IList<StockDrop> GetStockDropsForSpecificDate(DateTime date)
        {
            return ctx.StockDrops.Where(x => x.Date == date).ToList();
        }

        public StockDrop AddStockDrop(int streetNumber, string streetName, DateTime date, string postCode, int ParcelId)
        {
            //check that this stockdrop does not already exist
            var exists = GetStockDropByPostCodeAndDate(postCode, date);

            //if this is not null then return null because it already exists
            if (exists != null) return null;

            var s = new StockDrop
            {
                StreetNumber = streetNumber,
                StreetName = streetName,
                Date = date,
                PostCode = postCode,
                ParcelId = ParcelId
            };

            ctx.StockDrops.Add(s);
            ctx.SaveChanges();

            return s;
            
        }

        public StockDrop GetStockDropByPostCodeAndDate(string postCode, DateTime date)
        {
            var stockDrop = ctx.StockDrops.FirstOrDefault(x => x.PostCode == postCode && x.Date == date);
            
            return stockDrop;
        }

        public StockDrop UpdateStockDrop(StockDrop updated)
        {
            //check that the stockdrop already exists before it can be updated
            var s = GetStockDropByPostCodeAndDate(updated.PostCode, updated.Date);

            //if it does not exist then return null because it cannot be updated
            if (s == null) return null;

            s.StreetNumber = updated.StreetNumber;
            s.StreetName = updated.StreetName;
            s.Date = updated.Date;
            s.PostCode = updated.PostCode;
            s.ParcelId = updated.ParcelId;

            ctx.SaveChanges();
            return s;
        }

        public StockDrop GetStockDropById(int id)
        {
            return ctx.StockDrops.FirstOrDefault(x => x.Id == id);
        }

        public bool DeleteStockDrop(int id)
        {
            //check that this stock drop exists before it can be deleted
            var s = GetStockDropById(id);

            //if it does not exist then return null as it cannot be deleted
            if (s == null) return false;

            var deleted = ctx.StockDrops.Remove(s);

            ctx.SaveChanges();
            return true;
        
        }

        public IList<RecipeIngredients> GetAllRecipeIngredients()
        {
            return ctx.RecipeIngredients.ToList();
            
        }

        public IList<RecipeIngredients> GetRecipeIngredientsWithSpecificQty(int quantity)
        {
            return ctx.RecipeIngredients.Where( x=> x.IngredientQuantity == quantity).ToList();
        }

        public RecipeIngredients GetRecipeIngredientById(int id)
        {
            return ctx.RecipeIngredients.FirstOrDefault( x=> x.Id == id);
        }

        public RecipeIngredients AddIngredientToRecipeIngredients(int ingredientId, int recipeId, int ingredientQuantity)
        {
            //check is that the recipeingredient does not exist and if it is found then return null
            var re = ctx.RecipeIngredients
                        .FirstOrDefault( x => x.IngredientId == ingredientId &&
                                              x.RecipeId == recipeId);

            if (re != null) return null;

            //locate the recipe and the ingredient in their seperate tables
            var r = ctx.Recipes.FirstOrDefault ( r => r.Id == recipeId);
            var i = ctx.Ingredients.FirstOrDefault ( i => i.Id == ingredientId );

            //if either already exist ion the recipeingredients table then return null
            if ( r == null || i == null) return null;

            //add the new recipe ingredient
            var nre = new RecipeIngredients { RecipeId = r.Id, IngredientId = i.Id, IngredientQuantity = ingredientQuantity };
            ctx.RecipeIngredients.Add(nre);

            //save changes and return the new recipeingredient
            ctx.SaveChanges();
            return nre;


        }

        public bool RemoveIngredientFromRecipeIngredients(int ingredientId, int recipeId)
        {
            //check that the ingredient is already in the recipeingredient table
            var re = ctx.RecipeIngredients.FirstOrDefault(
                r => r.RecipeId == recipeId && r.IngredientId == ingredientId

            );

            if (re == null) return false;

            ctx.RecipeIngredients.Remove(re);
            ctx.SaveChanges();
            return true;
        }

        public RecipeIngredients UpdateRecipeIngredientsQuantity(int recipeId, int ingredientId, int ingredientQuantity)
        {
            //check that both the individual recipe and the ingredient exist
            var recipe = GetRecipeById(recipeId);

            if (recipe == null) return null;

            var re = recipe.RecipeIngredients.FirstOrDefault( o => o.IngredientId == ingredientId);

            if (re == null) return null;

            re.IngredientQuantity = ingredientQuantity;

            ctx.SaveChanges();
            return re;
        }

        IList<Recipe> GetAllRecipes()
        {
            return ctx.Recipes.ToList();
        }

        IList<Recipe> GetVegetarianRecipes(bool vegetarian)
        {
            return ctx.Recipes.Where(x => x.Vegetarian == true).ToList();
        }

        public IList<Recipe> GetCoeliacFriendlyRecipes(bool coeliacFriendly)
        {
            return ctx.Recipes.Where(x => x.CoeliacFriendly == true).ToList();
        }

        public IList<Recipe> GetRecipesSpecifiedByMeatType(string meatType)
        {
            return ctx.Recipes.Where(x => x.MeatType == meatType).ToList();
        }

        public IList<Recipe> GetRecipesUnderSpecificTime(int cookingTime)
        {
            return ctx.Recipes.Where(x => x.CookingTimeMins <= cookingTime).ToList();
        }

        public IList<Recipe> GetVegetarianRecipesUnderASpecificTime(bool vegetarian, int CookingTimeMins)
        {
            return ctx.Recipes.Where(x => x.Vegetarian == true && x.CookingTimeMins < CookingTimeMins).ToList();
        }

        public IList <Recipe> GetCoeliacRecipesUnderASpecificTime(bool coeliacFriendly, int cookingTimeMins)
        {
            return ctx.Recipes.Where(x => x.CoeliacFriendly == true && x.CookingTimeMins <= cookingTimeMins).ToList();
        }

        public IList<Recipe> GetCertainMeatUnderSpecificTime(string meatType, int cookingTimeMins)
        {
            return ctx.Recipes.Where(x => x.MeatType == meatType && x.CookingTimeMins <= cookingTimeMins).ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return ctx.Recipes.FirstOrDefault(x => x.Id == id);
        }

        public Recipe GetRecipeByName(string name)
        {
            return ctx.Recipes.FirstOrDefault(x => x.Name == name);
        }

        public Recipe AddRecipe(string name, bool vegetarian, bool coeliacFriendly, int cookingTimeMins, string meatType, int recipeIngredientsId)
        {
            //check the recipe does not exists using the recipe id
            var exists = GetRecipeByName(name);

            //if the recipe already exists then cannot add recipe so return null
            if (exists != null) return null;

            var recipe = new Recipe
            {
                Name = name,
                Vegetarian = vegetarian,
                CoeliacFriendly = coeliacFriendly,
                CookingTimeMins = cookingTimeMins,
                MeatType = meatType,
                RecipeIngredientsId = recipeIngredientsId

            };

            ctx.Add(recipe);
            ctx.SaveChanges();

            return recipe;
        }

        public Recipe UpDateRecipe(Recipe updated)
        {
            //check that the specifc recipe exists
            var recipe = GetRecipeById(updated.Id);

            if(recipe != null) return null;

            recipe.Name = updated.Name;
            recipe.Vegetarian = updated.Vegetarian;
            recipe.CoeliacFriendly = updated.CoeliacFriendly;
            recipe.MeatType = updated.MeatType;
            recipe.RecipeIngredientsId = updated.RecipeIngredientsId;

            ctx.SaveChanges();
            return recipe;

        }

        bool IUserService.DeleteRecipe(int id, string Name)
        {
            //find the recipe first before deleting
            var recipe = GetRecipeById(id);

            if (recipe == null) return false;

            var delete = ctx.Recipes.Remove(recipe);

            ctx.SaveChanges();
            return true;
        }
    }
}