using Microsoft.AspNetCore.Mvc;
using Repo.Interfaces;
using Service.Interfaces;
using System.Security.Claims;

namespace Cinema.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(currentUserId == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var items = await _shoppingCartService.GetCartItemsByUserId(currentUserId);

            return View(items);
        }
        [HttpPost]
        public async Task<int> AddTicketToCart(Guid ticketId, int? quantity)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _shoppingCartService.AddTicketToCart(ticketId, quantity, currentUserId);
        }
        [HttpPost]
        public async Task<int> AddOneItemToCart(int shoppingCartTicketId)
        {
            return await _shoppingCartService.AddOneItemToCart(shoppingCartTicketId);
        }
		[HttpPost]
		public async Task<int> RemoveOneItemFromCart(int shoppingCartTicketId)
		{
			return await _shoppingCartService.RemoveOneItemFromCart(shoppingCartTicketId);
		}
		[HttpPost]
		public async Task<int> RemoveTicketFromShoppingCart(int shoppingCartTicketId)
		{
			return await _shoppingCartService.RemoveTicketFromShoppingCart(shoppingCartTicketId);
		}

	}
}
