using System;
namespace FBMS.Core.Models
{
    // Each user split into different roles using enumeration
    //Different types of users will have different privileges in the application
    public enum Role { admin, manager, volunteer, client }

    //principal entity
    public class User
    {
        //suitable properties for each user
        public int Id { get; set; }

        //seperate first name and surname because it helps in normalisation
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // User role within application
        public Role Role { get; set; }

        public IList<DietaryRequirements> DietaryRequirements { get; set; } = new List<DietaryRequirements>();
        //each user of type client will have a list of tickets and parcels 
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();

        public IList<Parcel> Parcels {get; set; } = new List<Parcel>();
    }
}
