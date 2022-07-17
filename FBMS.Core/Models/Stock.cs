using System;
namespace FBMS.Core.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public bool FoodItem { get; set; }

        public DateOnly ExpiryDate { get; set; }

        //navigation from stock to parcel
        public Parcel Parcel { get; set; }

        //each item of stock will have 1 and only 1 parcel
        public int ParcelId { get; set; }

    }
}