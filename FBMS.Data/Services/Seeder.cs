
using FBMS.Core.Models;
using FBMS.Core.Services;

namespace FBMS.Data.Services
{
    public static class Seeder
    {
        //this class will seed dummy data into the database
         public static void Seed(IUserService svc)
        {
            svc.Initialise();

            // adding the users that are previously identified by the enumeration role
            svc.AddUser("Mr", "Admin", "BT49 0ST", "ceo@mail.com", "password", Role.admin);
            svc.AddUser("Mr", "Manager", "BT56 7PO", "manager@mail.com","password", Role.manager);
            svc.AddUser("Mr", "Supplier","BT90 5NM","supp@mail.com", "password1", Role.supplier);
            svc.AddUser("Mr", "Volunteer", "BT90 3LM", "vol@mail.com", "password2", Role.volunteer);
            svc.AddUser("Mr", "Client", "BT65 0OI", "client@mail.com", "password3", Role.client);    
        }
    }
}
