using System;
namespace FBMS.Core.Models
{

    public enum TicketRange { OPEN, CLOSED, ALL }
    //ticket is submitted by client user as a request for a food parcel
    public class Ticket
    {
        public int Id { get; set; }

        public int NoOfPeople { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime DateRequired { get; set; }

        //foreign key from the principal entity to the dependent entity
        public int UserId { get; set; }

        public User User { get; set; }

        public Parcel Parcel { get; set; }

        public int ParcelId { get; set; }

        public int DietaryRequirementId { get; set ;}

        public IList<DietaryRequirements> DietaryRequirements { get; set; } = new List<DietaryRequirements>();
    
    }

}