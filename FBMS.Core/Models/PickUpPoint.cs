using System;
namespace FBMS.Core.Models
{
    public class PickUpPoint
    {
        public int StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string PostCode { get; set; }
        
        public DateTime DateAndTime { get; set; }

    }
}