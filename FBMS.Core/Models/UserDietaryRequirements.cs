using System;
namespace FBMS.Core.Models
{
    public class UserDietaryRequirements
    {
        public int Id { get; set; }

        public string Description { get; set; }

        //Foreign key for the related User
        public int UserId { get; set; }

        //navigation property from UserDietaryRequirements To User
        public User User { get; set; }

        //foreign key for the related dietary requirements table
        public int DietaryRequirementsId { get; set; }

        //navgiation propert from UserDietaryRequirements To DietaryRequirements
        public DietaryRequirements DietaryRequirements { get; set; }

    }
}