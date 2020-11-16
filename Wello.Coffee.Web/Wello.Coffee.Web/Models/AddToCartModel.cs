using System.ComponentModel.DataAnnotations;

namespace Wello.Coffee.Web.Models
{
    public class AddToCartModel
    {
        public string DrinkType { get; set; }
        [Required]
        public string Size { get; set; }
        public int CreamQuantity { get; set; }
        public int SugarQuantity { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
