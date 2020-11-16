using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wello.Coffee.Web.Models;

namespace Wello.Coffee.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(CheckoutViewModel model)
        {            
            return View("Views/Checkout.cshtml", model);
        }
    }
}
