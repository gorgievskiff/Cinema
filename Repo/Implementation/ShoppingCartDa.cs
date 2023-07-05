using Domain.DomainModels;
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
    public class ShoppingCartDa : IShoppingCartDa
    {
        private static ILogger<ShoppingCartDa> _logger;
        private readonly ApplicationDbContext _db;
        public ShoppingCartDa(ILogger<ShoppingCartDa> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<int> Add(ShoppingCart shoppingCart)
        {
            try
            {
                _db.ShoppingCarts.Add(shoppingCart);
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> AddTicketToCart(Guid ticketId, int? quantity, string userId)
        {
            try
            {
                var shoppingCartTicket = new ShoppingCartTickets();
                var cart = await GetCartByUserId(userId);
                shoppingCartTicket.TicketId = ticketId;
                shoppingCartTicket.ShoppingCartId = cart.Id;
                if(quantity != null)
                {
                    shoppingCartTicket.Quantity = (int)quantity;
                }
                else
                {
                    shoppingCartTicket.Quantity = 1;
                }

                _db.ShoppingCartTickets.Add(shoppingCartTicket);
                return await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public Task<int> Delete(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShoppingCartTickets>> GetCartItemsByUserId(string userId)
        {
            try
            {
                var cart = await _db.ShoppingCarts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                var items = await _db.ShoppingCartTickets
                                    .Where(x => x.ShoppingCartId == cart.Id)
                                    .Include(x => x.Ticket)
                                    .Include(x => x.Ticket.Movie)
                                    .Include(x => x.Ticket.Movie.MovieGenres)
                                    .Where(x => !x.IsPaid)
                                    .ToListAsync();
                return items;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<int> AddOneItemToCart(int shoppingCartId)
        {
            try
            {
                var ticketCartFromDb = await _db.ShoppingCartTickets.Where(x => x.Id == shoppingCartId).FirstOrDefaultAsync();
                ticketCartFromDb.Quantity += 1;
                _db.Update(ticketCartFromDb);

                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
		public async Task<int> RemoveOneItemFromCart(int shoppingCartId)
		{
			try
			{
				var ticketCartFromDb = await _db.ShoppingCartTickets.Where(x => x.Id == shoppingCartId).FirstOrDefaultAsync();
                if(ticketCartFromDb.Quantity == 1)
                {
                    _db.ShoppingCartTickets.Remove(ticketCartFromDb);
                }
                else if(ticketCartFromDb.Quantity > 1)
                {
					ticketCartFromDb.Quantity -= 1;
					_db.Update(ticketCartFromDb);
				}

				return await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}
		public async Task<int> RemoveTicketFromShoppingCart(int shoppingCartId)
		{
			try
			{
				var ticketCartFromDb = await _db.ShoppingCartTickets.Where(x => x.Id == shoppingCartId).FirstOrDefaultAsync();
                _db.ShoppingCartTickets.Remove(ticketCartFromDb);

				return await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}


		public async Task<ShoppingCart> GetCartByUserId(string userId)
        {
            try
            {
                var cartFromDb = await _db.ShoppingCarts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (cartFromDb != null)
                {
                    return cartFromDb;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public Task<int> Update(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
