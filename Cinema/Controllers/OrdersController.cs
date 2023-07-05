using Domain.DomainModels;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service.Interfaces;
using System.Security.Claims;

namespace Cinema.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly IOrderService _orderService;
        public OrdersController(IShoppingCartService shoppingCartService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
			_orderService = orderService;
        }
        public async Task<IActionResult> Index()
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (currentUserId == null)
			{
				return Redirect("/Identity/Account/Login");
			}
			var items = await _shoppingCartService.GetCartItemsByUserId(currentUserId);

			return View(items);
		}

		public async Task<IActionResult> ListOrders()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var orders = await _orderService.GetAllPaidOrders(userId);
			return View(orders);
		}

		[HttpPost]
		public async Task<IActionResult> PayForTickets()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			 await _orderService.PayOrderInShoppingCart(userId);
			return RedirectToAction("ListOrders", "Orders");
		}
	}
}
