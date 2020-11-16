using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Wello.Coffee.Web.Models;
using Wello.Coffee.Web.Models.Config;

namespace Wello.Coffee.Web.Services
{
    public class HomeService : IHomeService
    {
        private readonly IOptions<ProductConfig> _productConfig;

        public HomeService(
            IOptions<ProductConfig> productConfig
            )
        {
            _productConfig = productConfig;
            // This is where the data context would be injected
        }

        // This would go to a database to get all product info. I will use json file.
        public VendingMachineViewModel GetVendingItems()
        {
            var vendingItems = JsonConvert
                .DeserializeObject<VendingMachineViewModel>(File.ReadAllText(_productConfig.Value.BaseAddress));

            return vendingItems;
        }

        public decimal GetDrinkItemPrice(DrinkCartDetail drinkCartDetail)
        {
            var vendingItems = JsonConvert
                .DeserializeObject<VendingMachineViewModel>(File.ReadAllText(_productConfig.Value.BaseAddress));

            var sizePrice = vendingItems.Drink.Sizes
                .Where(x => x.Code == drinkCartDetail.SizeCode)
                .FirstOrDefault().Price;

            var creamPrice = vendingItems.Drink.Cream.Price;
            var sugarPrice = vendingItems.Drink.Sugar.Price;

            return sizePrice + (creamPrice * drinkCartDetail.CreamQuantity)
                + (sugarPrice * drinkCartDetail.SugarQuantity);
        }
    }
}
