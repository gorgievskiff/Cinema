using Domain.DomainModels;
using Domain.DTO;
using Repo.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
	public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartDa _shoppingCartDa;
        public ShoppingCartService(IShoppingCartDa shoppingCartDa)
        {
            _shoppingCartDa = shoppingCartDa;
        }
        public async Task<int> Add(ShoppingCart shoppingCart)
        {
            return await _shoppingCartDa.Add(shoppingCart);
        }

        public Task<int> Delete(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCartTicketsDto> GetCartItemsByUserId(string userId)
        {
            var items = await _shoppingCartDa.GetCartItemsByUserId(userId);
            var dto = new ShoppingCartTicketsDto();
            foreach(var item in items)
            {
                dto.Tickets.Add(new ShoppingCartTickets
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    ShoppingCartId = item.Id,
                    ShoppingCart = item.ShoppingCart,
                    Ticket = item.Ticket,
                    TicketId = item.TicketId
                });
                dto.TotalSum += (item.Quantity * item.Ticket.Price);
            }
            return dto;

        }

        public Task<int> Update(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddTicketToCart(Guid ticketId, int? quantity, string userId)
        {
            return await _shoppingCartDa.AddTicketToCart(ticketId, quantity, userId);
        }

        public Task<int> AddTicketToCart(Guid ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddOneItemToCart(int shoppingCartId)
        {
            return await _shoppingCartDa.AddOneItemToCart(shoppingCartId);
        }

		public async Task<int> RemoveOneItemFromCart(int shoppingCartId)
		{
			return await _shoppingCartDa.RemoveOneItemFromCart(shoppingCartId);
		}

		public async Task<int> RemoveTicketFromShoppingCart(int shoppingCartId)
		{
            return await _shoppingCartDa.RemoveTicketFromShoppingCart(shoppingCartId);
		}


	}
}
