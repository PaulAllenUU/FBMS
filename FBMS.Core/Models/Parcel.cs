using System;
namespace FBMS.Core.Models
{
    public class Parcel
    {
        public int Id { get; set; }

        public bool Collected { get; set; }

        //foreign key from the stockdrop table to the parcel table
        public StockDrop StockDrop { get; set; }

        //each Parcel will have 1 and only 1 StockDrop
        public int StockDropId { get; set; }

        public Ticket Ticket { get; set; }

        public int TicketId { get; set; }

        public IList<Stock> Stock { get; set; } = new List<Stock>();
    }
}