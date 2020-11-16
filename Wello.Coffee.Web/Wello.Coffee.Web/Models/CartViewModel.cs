using System.Collections.Generic;

namespace Wello.Coffee.Web.Models
{
    public class CartViewModel
    {
        public decimal TotalCost { get; set; }

        public List<CartItem> CartItems = new List<CartItem>();
    }

    public class CartItem
    {
        public DrinkCartDetail DrinkDetails { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total => SubTotal * Quantity;
    }

    public class DrinkCartDetail
    {
        public string DrinkType { get; set; }

        public string SizeCode { get; set; }

        public int CreamQuantity { get; set; }

        public int SugarQuantity { get; set; }
    }
}
