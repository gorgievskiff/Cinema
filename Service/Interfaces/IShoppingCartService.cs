using Domain.DomainModels;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShoppingCartService
    {
        Task<int> Add(ShoppingCart shoppingCart);
        Task<int> Delete(int shoppingCartId);
        Task<int> Update(ShoppingCart shoppingCart);
        Task<ShoppingCartTicketsDto> GetCartItemsByUserId(string userId);
		Task<int> AddTicketToCart(Guid ticketId, int? quantity, string userId);
        Task<int> AddOneItemToCart(int shoppingCartId);
        Task<int> RemoveOneItemFromCart(int shoppingCartId);
		Task<int> RemoveTicketFromShoppingCart(int shoppingCartId);


	}
}
