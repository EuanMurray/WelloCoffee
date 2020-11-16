using Wello.Coffee.Web.Models;

namespace Wello.Coffee.Web.Services
{
    public interface IHomeService
    {
        VendingMachineViewModel GetVendingItems();

        decimal GetDrinkItemPrice(DrinkCartDetail drinkCartDetail);
    }
}
