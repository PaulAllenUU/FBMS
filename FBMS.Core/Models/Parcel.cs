using System;
namespace FBMS.Core.Models
{
    public class Parcel
    {
        public int Id { get; set; }

        public DateTime DateGiven { get; set; }

        public string ItemsIncluded { get; set; }

        public int Quantity { get; set; }

        public bool Collected { get; set; }

        //foreign key from the user table to the parcel table 
        public int UserId { get; set; }

        public DropOffPoint DropOffPoint { get; set; }

        public User Volunteer { get; set; }

        public Ticket Ticket { get; set; }

        public int TicketId { get; set; }
    }
}