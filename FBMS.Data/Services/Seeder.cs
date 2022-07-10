
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
            svc.AddUser("Administrator", "admin@mail.com", "admin", Role.admin);
            svc.AddUser("Manager", "manager@mail.com", "volunteer", Role.manager);
            svc.AddUser("Supplier", "supplier@mail.com", "supplier", Role.supplier);
            svc.AddUser("Volunteer", "volunteer@mail.com", "volunteer", Role.volunteer);
            svc.AddUser("Client", "client@mail.com", "guest", Role.client);    
        }
    }
}
