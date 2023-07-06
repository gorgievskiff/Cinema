using Domain.DomainModels;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Implementation
{
	public class OrderDa : IOrderDa
	{
		private readonly ApplicationDbContext _db;
		private readonly ILogger<OrderDa> _logger;
        public OrderDa(ApplicationDbContext db, ILogger<OrderDa> logger)
        {
            _db = db;
			_logger = logger;
        }
        public async Task<int> PayOrderInShoppingCart(string userId)
		{
			try
			{
				var shoppingCart = await _db.ShoppingCarts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
				var unpaidTickets = await _db.ShoppingCartTickets.Include(x => x.Ticket).Where(x => x.ShoppingCartId == shoppingCart.Id && !x.IsPaid).ToListAsync();
				var totalSum = 0.0;
				foreach (var ticket in unpaidTickets)
				{
					totalSum += (ticket.Quantity * ticket.Ticket.Price);
				}
				var order = new Order();
				order.TotalSum = totalSum;
				order.OrderDate = DateTime.Now;
				order.UserId = userId;

				_db.Orders.Add(order);
				await _db.SaveChangesAsync();

				var orderItems = new List<OrderItems>();
				foreach(var ticket in unpaidTickets)
				{
					orderItems.Add(new OrderItems
					{
						OrderId = order.Id,
						ShoppingCartTicketsId = ticket.Id,
					});
					ticket.IsPaid = true;
				}

				_db.OrderItems.AddRange(orderItems);
				_db.ShoppingCartTickets.UpdateRange(unpaidTickets);
				return await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}
		public async Task<List<ListPaidOrdersDto>> GetAllPaidOrders(string userId)
		{
			try
			{
				var shoppingCart = await _db.ShoppingCarts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
				var orders = await _db.Orders.Include(x => x.OrderedItems).Where(x => x.UserId == userId).ToListAsync();
				var listPaidOrdersDto = new List<ListPaidOrdersDto>();
				foreach (var order in orders)
				{
					var paidOrder = new ListPaidOrdersDto();
					var orderedItems = await _db.OrderItems
						.Include(x => x.ShoppingCartTickets)
						.Include(x => x.ShoppingCartTickets.Ticket)
						.Include(x => x.ShoppingCartTickets.ShoppingCart)
                        .Where(x => x.OrderId == order.Id && x.ShoppingCartTickets.ShoppingCart.UserId == userId).ToListAsync();

					if(orderedItems.Count > 0)
					{
                        paidOrder.TotalSum = order.TotalSum;
                        paidOrder.OrderDate = order.OrderDate;
						paidOrder.OrderId = order.Id;
                    }

                    var listItems = new List<PaidMovieTicketDto>();

					foreach(var item in orderedItems)
					{
						var ticket = await _db.ShoppingCartTickets
							.Include(x => x.Ticket)
							.Include(x => x.Ticket.Movie)
							.Include(x => x.ShoppingCart)
							.Where(x => x.Id == item.ShoppingCartTicketsId)
							.FirstOrDefaultAsync();
						listItems.Add(new PaidMovieTicketDto
						{
							ImgUrl = ticket.Ticket.Movie.ImgUrl,
							MovieName = ticket.Ticket.Movie.Name,
							Price = ticket.Ticket.Price,
							Quantity = ticket.Quantity
						});
					}
					paidOrder.PaidTicketMovies = listItems;
					listPaidOrdersDto.Add(paidOrder);
				}
				return listPaidOrdersDto;
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}
        public async Task<string> GetUserEmailByUserId(string userId)
        {
            try
            {
                return await _db.Users.Where(x => x.Id == userId).Select(x => x.Email).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
