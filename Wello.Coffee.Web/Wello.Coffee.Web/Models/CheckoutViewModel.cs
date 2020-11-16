namespace Wello.Coffee.Web.Models
{
    public class CheckoutViewModel
    {
        public CartViewModel Cart { get; set; }

        public decimal LeftToPay { get; set; }

        public decimal HavePaid { get; set; }


    }
}
