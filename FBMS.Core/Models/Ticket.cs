using System;
namespace FBMS.Core.Models
{

    public class Ticket
    {
        public string Id { get; set; }

        public string DietaryRequirements { get; set; }

        public string NoOfPeople { get; set; }

        public DateTime DateTicketSubmiited { get; set; }

        public DateTime DateParcelRequired { get; set; }

        //foreign key from the principal entity to the dependent entity
        public int UserId { get; set; }

        public User Client { get; set; }

        public Parcel Parcel { get; set; }
    
    }

}