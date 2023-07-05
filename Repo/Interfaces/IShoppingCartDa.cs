using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface IShoppingCartDa
    {
        Task<int> Add(ShoppingCart shoppingCart);
        Task<int> Delete(int shoppingCartId);
        Task<int> Update(ShoppingCart shoppingCart);
        Task<ShoppingCart> GetCartByUserId(string userId);
        Task<int> AddTicketToCart(Guid ticketId, int? quantity, string userId);
        Task<List<ShoppingCartTickets>> GetCartItemsByUserId(string userId);
        Task<int> AddOneItemToCart(int shoppingCartId);
        Task<int> RemoveOneItemFromCart(int shoppingCartId);
        Task<int> RemoveTicketFromShoppingCart(int shoppingCartId);

	}
}
