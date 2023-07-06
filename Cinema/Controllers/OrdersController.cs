using Domain.DomainModels;
using Domain.DTO;
using MailKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service.Interfaces;
using System.Security.Claims;
using Service;
using System.Text;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    [Authorize]
	public class OrdersController : Controller
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly IOrderService _orderService;
		private readonly IEmailServiceOwn _emailSender;
        private IConverter _converter;
        public OrdersController(IShoppingCartService shoppingCartService, IOrderService orderService, IEmailServiceOwn emailSender, IConverter converter)
        {
            _shoppingCartService = shoppingCartService;
			_orderService = orderService;
			_emailSender = emailSender;
            _converter = converter;
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
			var userEmail = await _orderService.GetUserEmailByUserId(userId);
            var message = new Message(new string[] { userEmail }, "Sucessfully created an order!", "This is the content from our email.");
            _emailSender.SendEmail(message);
            await _orderService.PayOrderInShoppingCart(userId);
			return RedirectToAction("ListOrders", "Orders");
		}

		[HttpGet]
		public async Task<IActionResult> PdfExport(int orderId)
		{
            var sb = new StringBuilder();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllPaidOrders(userId);

            var order = orders.Where(x => x.OrderId == orderId).FirstOrDefault();

            sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'>{0}</h3></div>
                                <table align='center'>
                                    <tr>
                                        <th>Movie Name</th>
                                        <th>Quantity</th>
                                        <th>Price</th>
                                        <th>Total</th>
                                    </tr>",order.OrderDate.ToString("dd.MM.yyyy"));
            foreach (var movie in order.PaidTicketMovies)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", movie.MovieName, movie.Quantity, movie.Price, movie.Quantity * movie.Price);
            }

           
            sb.AppendFormat(@"
                                </table>
                    <div>
                        <h3>
                           Total: {0}$
                        </h3>
                    </div>
                            </body>
                        </html>",order.TotalSum);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = sb.ToString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "css", "bootstrap.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");

        }

    }
}
