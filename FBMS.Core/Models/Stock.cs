using System;
namespace FBMS.Core.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool FoodItem { get; set; }

        public DateTime ExpiryDate { get; set; }

    }
}