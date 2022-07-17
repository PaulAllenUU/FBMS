using System;
namespace FBMS.Core.Models
{
    public class StockDrop
    {
        public int Id { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }

        public DateTime Date { get; set; }

        public string PostCode { get; set; }

        //the many side of the relationship between parcel & stockdrop
        public Parcel Parcel { get; set; }

        public int ParcelId { get; set; }

        //Each stopdrop will have a list containing 1 or more parcels
        public IList<Parcel> Parcels { get; set; } = new List<Parcel>();


    }
}