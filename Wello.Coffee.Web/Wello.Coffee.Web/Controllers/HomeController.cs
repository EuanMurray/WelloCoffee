using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wello.Coffee.Web.Helpers;
using Wello.Coffee.Web.Models;
using Wello.Coffee.Web.Services;

namespace Wello.Coffee.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult IndexAsync()
        {
            // Gets vending machine items from data store
            var model = _homeService.GetVendingItems();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Adds item to cart
        [HttpPost]
        public IActionResult AddToCart(AddToCartModel model)
        {
            var cart = new CartViewModel();

            var drinkItem = new DrinkCartDetail
            {
                DrinkType = model.DrinkType,
                SizeCode = model.Size,
                CreamQuantity = model.CreamQuantity,
                SugarQuantity = model.SugarQuantity
            };

            // Check if cart cookie exists
            var cookieKey = Constants.CookieName;
            var cookie = HttpContext.Request.Cookies.ContainsKey(cookieKey)
                ? HttpContext.Request.Cookies[cookieKey]
                : string.Empty;

            // If cookie exists - check if drink combination exists
            //      Yes - Add to quantity
            //      No - Add to cart
            if (!string.IsNullOrEmpty(cookie))
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(cookie);

                var sameCartItem = cart.CartItems
                    .Where(x => x.DrinkDetails.CreamQuantity == drinkItem.CreamQuantity
                && x.DrinkDetails.DrinkType == drinkItem.DrinkType
                && x.DrinkDetails.SizeCode == drinkItem.SizeCode
                && x.DrinkDetails.SugarQuantity == drinkItem.SugarQuantity);

                if (sameCartItem != null && sameCartItem.Count() > 0)
                {
                    cart.CartItems.Where(x => x.DrinkDetails.CreamQuantity == drinkItem.CreamQuantity
                && x.DrinkDetails.DrinkType == drinkItem.DrinkType
                && x.DrinkDetails.SizeCode == drinkItem.SizeCode
                && x.DrinkDetails.SugarQuantity == drinkItem.SugarQuantity).FirstOrDefault().Quantity += model.Quantity;                    
                }
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        DrinkDetails = drinkItem,
                        SubTotal = _homeService.GetDrinkItemPrice(drinkItem),
                        Quantity = model.Quantity
                    });
                }
            }
            else
            {
                // Add to new cart item if no cart exists
                cart.CartItems.Add(new CartItem
                {
                    DrinkDetails = drinkItem,
                    SubTotal = _homeService.GetDrinkItemPrice(drinkItem),
                    Quantity = model.Quantity
                });
            }

            // Get cart total
            cart.TotalCost = cart.CartItems.Sum(x => x.Total);

            // Create/Update Cookie
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Response.Cookies.Append(cookieKey, cartJson);

            var vendingMachine = _homeService.GetVendingItems();
            vendingMachine.Cart = cart;

            return View("Views/Home/Index.cshtml", vendingMachine);
        }
    }
}
